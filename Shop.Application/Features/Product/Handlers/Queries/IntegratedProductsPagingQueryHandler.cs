using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Convertor;
using Shop.Application.Dapper.Helper;
using Shop.Application.Dapper.Product.Queries;
using Shop.Application.DTOs.Api;
using Shop.Application.DTOs.Product;
using Shop.Application.Enum;
using Shop.Application.Features.Product.Requests.Queries;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace Shop.Application.Features.Product.Handlers.Queries
{
    public class IntegratedProductsPagingQueryHandler : IRequestHandler<IntegratedProductsPagingQueryReq, ProductPageingDto>
    {
        #region Constractor
        private readonly IProductRepository _productRep;
        private readonly ICateguryRepository _categuryRep;
        private readonly IUserStoreRepository _userStoreRep;
        private readonly IUserRepository _userRep;
        private readonly IProductQueries _productQuery;
        private readonly string _connectionString;

        public IntegratedProductsPagingQueryHandler(IProductRepository productRep, ICateguryRepository categuryRep, IUserStoreRepository userStoreRep, IConfiguration configuration, IProductQueries productQuery, IUserRepository userRep)

        {
            _productRep = productRep;
            _categuryRep = categuryRep;
            _userStoreRep = userStoreRep;
            _connectionString = configuration.GetConnectionString("DatabaseConnection");
            _productQuery = productQuery;
            _userRep = userRep;
        }
        #endregion
        public async Task<ProductPageingDto> Handle(IntegratedProductsPagingQueryReq request, CancellationToken cancellationToken)
        {

            ProductPageingDto response = new ProductPageingDto();
            var products = new List<ProductDto>();
            var categury = "";
            var orderBy = "";
            var search = "";
            var condition = new StringBuilder();
            var paging = "";
            var joinType = "";

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
                var store = await _userStoreRep.GetStoreByLatinName(request.parameters.Store);
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
            var categuryIdQuery = new StringBuilder();
            
            if (request.parameters.Categury != null)
            {
                var cat = await _categuryRep.GetCateguryByLatinName(request.parameters.Categury);
                categuryIdQuery.Append($" CateguryId = {cat.GroupId} ");
                if (cat.IsParnet)
                {
                    var cats = await _categuryRep.GetCateguryByParentId(cat.GroupId, null);
                    foreach (var ca in cats)
                    {
                        categuryIdQuery.Append($"Or CateguryId = {ca.GroupId}");
                    }
                }
                categury = $"AND EXISTS (SELECT 1 FROM dbo.ProductCategury WHERE ProductId = p.Id AND ({categuryIdQuery})) ";
            }
            else if (request.parameters.CateguryId != 0)
            {
                categury = $"AND EXISTS (SELECT 1 FROM dbo.ProductCategury WHERE ProductId = p.Id AND (CateguryId = {request.parameters.CateguryId})) ";
            }
            #endregion

            #region Serach
            if (request.parameters.Filter != null)
            {
                var words = GetWordFromString.GetWords(request.parameters.Filter);
                var sb = new StringBuilder();
                if (words.Count() > 1 && words.Count() < 4)
                {
                    sb.Append($"\"{request.parameters.Filter}*\"OR");


                    foreach (var word in words)
                    {

                        var lastItem = words.Last();
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
                var existUserCategury = await _userRep.ExistUserCategury(request.userName);
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

            var list = await _productRep.GetProductsPaging(joinType, search, categury, condition.ToString(), orderBy, paging);
            foreach (var product in list)
            {
                #region Image Of List
                var image = DapperHelper.ExecuteCommand<Domain.Models.ProductGallery>(_connectionString, conn => conn.Query<Domain.Models.ProductGallery>(_productQuery.GetMainImage, new { @productId = product.ProductId }).FirstOrDefault());
                if (image != null)
                {
                    product.Image = image.ImageName;
                }

                #endregion
                #region User Categury

                if (product.CateguryOfUserId != null)
                {
                    product.CateguryOfUserName=await _userRep.GetUserCategury(product.CateguryOfUserId.Value);
                }


                #endregion
                #region Check UserCategury

                var existUserCategury = false;
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
}
