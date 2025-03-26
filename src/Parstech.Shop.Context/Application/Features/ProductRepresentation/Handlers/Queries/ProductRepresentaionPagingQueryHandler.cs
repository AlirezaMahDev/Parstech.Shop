using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Product;
using Parstech.Shop.Context.Application.DTOs.ProductRepresentation;
using Parstech.Shop.Context.Application.Features.ProductRepresentation.Requests.Queries;

using AutoMapper;

using Parstech.Shop.Context.Application.Dapper.Product.Queries;
using Microsoft.Extensions.Configuration;
using Parstech.Shop.Context.Application.Dapper.Helper;
using Dapper;

namespace Parstech.Shop.Context.Application.Features.ProductRepresentation.Handlers.Queries;

public class ProductRepresentaionPagingQueryHandler : IRequestHandler<ProductRepresentaionPagingQueryReq, ProductRepresentationPagingDto>
{
    private readonly IProductRepresesntationRepository _productRepresentationRep;
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRep;
    private readonly IProductStockPriceRepository _productStockRep;
    private readonly IProductQueries _productQueries;
    private readonly string _connectionString;

    public ProductRepresentaionPagingQueryHandler(IProductRepresesntationRepository productRepresentationRep,
        IMapper mapper,
        IProductRepository productRep,
        IProductQueries productQueries,
        IConfiguration configuration,
        IProductStockPriceRepository productStockRep)
    {
        _productRepresentationRep = productRepresentationRep;
        _mapper = mapper;
        _productRep = productRep;
        _productStockRep = productStockRep;
        _productQueries = productQueries;
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }
    public async Task<ProductRepresentationPagingDto> Handle(ProductRepresentaionPagingQueryReq request, CancellationToken cancellationToken)
    {
        ProductRepresentationPagingDto response = new();
        string serach = "";
        string categury = "";
        string type = "";
        string exist = "";
            
        if (!string.IsNullOrEmpty(request.ProductRepresenationParameterDto.Filter))
        {
            serach = $" AND dbo.Product.Id={request.ProductRepresenationParameterDto.Filter}";
        }
        if (request.ProductRepresenationParameterDto.Categury != 0)
        {
            categury = $"AND EXISTS (SELECT 1 FROM dbo.ProductCategury WHERE ProductId = dbo.ProductStockPrice.ProductId AND CateguryId = {request.ProductRepresenationParameterDto.Categury}) ";
        }
        if (request.ProductRepresenationParameterDto.Type != null)
        {
            switch (request.ProductRepresenationParameterDto.Type)
            {
                case "Discount":
                    type = $"AND dbo.ProductStockPrice.DiscountPrice!=0";
                    break;
                case "NotDiscount":
                    type = $"AND dbo.ProductStockPrice.DiscountPrice=0";
                    break;
            }

        }
        if (request.ProductRepresenationParameterDto.Exist != null)
        {
            switch (request.ProductRepresenationParameterDto.Exist)
            {
                case "Exist":
                    exist = $"AND dbo.ProductStockPrice.Quantity!=0";
                    break;
                case "NotExist":
                    exist = $"AND dbo.ProductStockPrice.Quantity=0";
                    break;
            }

        }

            
        int skip = (request.ProductRepresenationParameterDto.CurrentPage - 1) * request.ProductRepresenationParameterDto.TakePage;
        var query = $"SELECT dbo.Product.Name,dbo.Product.SingleSale, dbo.Product.LatinName, dbo.Product.Code, dbo.Product.ShortDescription, dbo.Product.ShortLink, dbo.Product.TypeId, dbo.ProductStockPrice.Id, dbo.ProductStockPrice.ProductId, dbo.ProductStockPrice.SalePrice,dbo.ProductStockPrice.DiscountPrice, dbo.ProductStockPrice.StockStatus, dbo.ProductStockPrice.Quantity, dbo.ProductStockPrice.MaximumSaleInOrder,dbo.Brand.BrandId,dbo.Brand.BrandTitle,dbo.Brand.LatinBrandTitle,dbo.UserStore.LatinStoreName, dbo.ProductStockPrice.StoreId, dbo.ProductStockPrice.RepId ,dbo.UserStore.StoreName FROM dbo.Product INNER JOIN dbo.ProductStockPrice ON dbo.Product.Id = dbo.ProductStockPrice.ProductId INNER JOIN dbo.UserStore ON dbo.ProductStockPrice.StoreId = dbo.UserStore.Id  INNER JOIN dbo.Brand ON dbo.Product.BrandId = dbo.Brand.BrandId where ProductStockPrice.RepId={request.ProductRepresenationParameterDto.RepId} and TypeId!=3 {categury} {type} {exist} {serach} ORDER BY CreateDate Desc OFFSET {skip} ROWS FETCH NEXT {request.ProductRepresenationParameterDto.TakePage} ROWS ONLY";
        var productReps = DapperHelper.ExecuteCommand<List<DapperProductDto>>(_connectionString, conn => conn.Query<DapperProductDto>(query).ToList());





        var productDtos = new List<ProductDto>();
        foreach (var item in productReps)
        {
            var Pdto = _mapper.Map<ProductDto>(item);
            Pdto.ProductStockPriceId = item.Id;
            productDtos.Add(Pdto);
            if (Pdto.TypeId == 2)
            {
                var variations = DapperHelper.ExecuteCommand<List<DapperProductDto>>(_connectionString, conn => conn.Query<DapperProductDto>(_productQueries.GetListVariationByParentId, new { @rep = request.ProductRepresenationParameterDto.RepId, @parentId = Pdto.ProductId }).ToList());
                foreach (var variation in variations)
                {
                    var vdto = _mapper.Map<ProductDto>(variation);
                    vdto.ProductStockPriceId = variation.Id;

                    productDtos.Add(vdto);
                }

            }

        }



        var AllListquery = $"SELECT dbo.Product.Id FROM dbo.Product INNER JOIN dbo.ProductStockPrice ON dbo.Product.Id = dbo.ProductStockPrice.ProductId INNER JOIN dbo.UserStore ON dbo.ProductStockPrice.StoreId = dbo.UserStore.Id  INNER JOIN dbo.Brand ON dbo.Product.BrandId = dbo.Brand.BrandId where ProductStockPrice.RepId={request.ProductRepresenationParameterDto.RepId} and TypeId!=3 {categury} {type} {exist} {serach} ORDER BY CreateDate Desc OFFSET {skip} ROWS FETCH NEXT {request.ProductRepresenationParameterDto.TakePage} ROWS ONLY";
        var AllList = DapperHelper.ExecuteCommand<List<int>>(_connectionString, conn => conn.Query<int>(AllListquery).ToList());
        response.CurrentPage = request.ProductRepresenationParameterDto.CurrentPage;
        response.PageCount = AllList.Count() / request.ProductRepresenationParameterDto.TakePage;


        response.List = productDtos.ToArray();

        return response;


    }
}