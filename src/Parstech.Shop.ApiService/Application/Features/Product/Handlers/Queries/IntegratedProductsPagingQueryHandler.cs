using Dapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Convertor;
using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.Dapper.Product.Queries;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Enum;
using Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;

using System.Text;

namespace Parstech.Shop.ApiService.Application.Features.Product.Handlers.Queries;

public class IntegratedProductsPagingQueryHandler : IRequestHandler<IntegratedProductsPagingQueryReq, ProductPageingDto>
{
    #region Constractor

    private readonly IProductRepository _productRep;
    private readonly ICateguryRepository _categuryRep;
    private readonly IUserStoreRepository _userStoreRep;
    private readonly IUserRepository _userRep;
    private readonly IProductQueries _productQuery;
    private readonly string _connectionString;

    public IntegratedProductsPagingQueryHandler(IProductRepository productRep,
        ICateguryRepository categuryRep,
        IUserStoreRepository userStoreRep,
        IConfiguration configuration,
        IProductQueries productQuery,
        IUserRepository userRep)

    {
        _productRep = productRep;
        _categuryRep = categuryRep;
        _userStoreRep = userStoreRep;
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
        _productQuery = productQuery;
        _userRep = userRep;
    }

    #endregion

    public async Task<ProductPageingDto> Handle(IntegratedProductsPagingQueryReq request,
        CancellationToken cancellationToken)
    {
        ProductPageingDto response = new();
        List<ProductDto> products = new();
        string categury = "";
        string orderBy = "";
        string search = "";
        StringBuilder condition = new();
        string paging = "";
        string joinType = "";

        #region Condution

        switch (request.parameters.IsActive)
        {
            case "Active":
                condition.Append("IsActive=1");
                break;
            case "NotActive":
                condition.Append("IsActive=0");

                break;
            default:
                condition.Append("IsActive=1");
                break;
        }

        if (request.parameters.Store != null)
        {
            Domain.Models.UserStore store = await _userStoreRep.GetStoreByLatinName(request.parameters.Store);
            condition.Append($"AND us.StoreId={store.Id}");
        }

        if (request.parameters.Exist)
        {
            condition.Append("and sp.Quantity>0");
        }

        if (request.parameters.MinPrice != 0)
        {
            condition.Append($"and sp.SalePrice >={request.parameters.MinPrice}");
        }

        if (request.parameters.MaxPrice != 0)
        {
            condition.Append($"and sp.SalePrice<={request.parameters.MaxPrice}");
        }

        #endregion

        #region Categury

        StringBuilder categuryIdQuery = new();

        if (request.parameters.Categury != null)
        {
            Domain.Models.Categury? cat = await _categuryRep.GetCateguryByLatinName(request.parameters.Categury);
            categuryIdQuery.Append($" CateguryId = {cat.GroupId} ");
            if (cat.IsParnet)
            {
                List<Domain.Models.Categury> cats = await _categuryRep.GetCateguryByParentId(cat.GroupId, null);
                foreach (Domain.Models.Categury ca in cats)
                {
                    categuryIdQuery.Append($"Or CateguryId = {ca.GroupId}");
                }
            }

            categury =
                $"AND EXISTS (SELECT 1 FROM dbo.ProductCategury WHERE ProductId = p.Id AND ({categuryIdQuery})) ";
        }
        else if (request.parameters.CateguryId != 0)
        {
            categury =
                $"AND EXISTS (SELECT 1 FROM dbo.ProductCategury WHERE ProductId = p.Id AND (CateguryId = {request.parameters.CateguryId})) ";
        }

        #endregion

        #region Serach

        if (request.parameters.Filter != null)
        {
            string[] words = GetWordFromString.GetWords(request.parameters.Filter);
            StringBuilder sb = new();
            if (words.Count() > 1 && words.Count() < 4)
            {
                sb.Append($"\"{request.parameters.Filter}*\"OR");


                foreach (string word in words)
                {
                    string lastItem = words.Last();
                    if (word == lastItem)
                    {
                        sb.Append($"\"{word}*\"");
                    }
                    else
                    {
                        sb.Append($"\"{word}*\" OR");
                    }
                }

                search = $"and CONTAINS(p.Name, '{sb}')";

                //search = $"and p.Name LIKE N'%{request.parameters.Filter}%'";
            }
            else
            {
                search = $"and p.Name LIKE N'%{request.parameters.Filter}%'";
            }
        }

        #endregion

        #region Order By

        switch (request.parameters.Type)
        {
            case "Top":
                orderBy = "ORDER BY sp.Quantity DESC, sp.SalePrice DESC ";
                break;
            case "New":
                orderBy = "ORDER BY p.CreateDate Desc";
                break;
            case "LowPrice":
                orderBy = "ORDER BY sp.Quantity DESC, sp.SalePrice ASC ";
                break;
            case "HighPrice":
                orderBy = "ORDER BY sp.Quantity DESC, sp.SalePrice DESC ";
                break;
            default:
                orderBy = "ORDER BY sp.Quantity DESC, sp.SalePrice ASC";
                break;
        }

        #endregion

        #region Pagination

        paging = $"OFFSET {request.parameters.Skip} ROWS FETCH NEXT {request.parameters.Take} ROWS ONLY;";

        #endregion

        #region Check UserCategury

        if (request.userName != null)
        {
            bool existUserCategury = await _userRep.ExistUserCategury(request.userName);
            if (existUserCategury)
            {
                joinType = "p.BestStockUserCateguryId";
            }
            else
            {
                joinType = "p.BestStockId";
            }
        }
        else
        {
            joinType = "p.BestStockId";
        }

        #endregion

        List<ProductDto> list =
            await _productRep.GetProductsPaging(joinType, search, categury, condition.ToString(), orderBy, paging);
        foreach (ProductDto product in list)
        {
            #region Image Of List

            Domain.Models.ProductGallery image = DapperHelper.ExecuteCommand<Domain.Models.ProductGallery>(
                _connectionString,
                conn => conn
                    .Query<Domain.Models.ProductGallery>(_productQuery.GetMainImage,
                        new { @productId = product.ProductId })
                    .FirstOrDefault());
            if (image != null)
            {
                product.Image = image.ImageName;
            }

            #endregion

            #region User Categury

            if (product.CateguryOfUserId != null)
            {
                product.CateguryOfUserName = await _userRep.GetUserCategury(product.CateguryOfUserId.Value);
            }

            #endregion

            #region Check UserCategury

            bool existUserCategury = false;
            if (request.userName != null)
            {
                existUserCategury = await _userRep.ExistUserCategury(request.userName);
            }

            if (product.CateguryOfUserId != null)
            {
                if (!existUserCategury)
                {
                    if (product.CateguryOfUserType == CateguryOfUserType.ShowDiscoutProductForUserCategury.ToString())
                    {
                        product.DiscountPrice = 0;
                        //product.SalePrice = productStockPrice.SalePrice;
                        //product.Quantity = productStockPrice.Quantity;
                    }
                    else if (product.CateguryOfUserType == CateguryOfUserType.ShowProductJustForUserCategury.ToString())
                    {
                        product.DiscountPrice = 0;
                        product.SalePrice = 0;
                        product.Quantity = 0;
                    }
                }
                //else
                //{
                //    product.DiscountPrice = productStockPrice.DiscountPrice;
                //    product.SalePrice = productStockPrice.SalePrice;
                //    product.Quantity = productStockPrice.Quantity;
                //}
            }

            //else
            //{
            //    product.DiscountPrice = productStockPrice.DiscountPrice;
            //    product.SalePrice = productStockPrice.SalePrice;
            //    product.Quantity = productStockPrice.Quantity;
            //}

            #endregion
        }


        response.ProductDtos = list.ToArray();
        response.ProductList = list;

        return response;
    }
}