using AutoMapper;
using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;

using Parstech.Shop.Context.Application.Dapper.Helper;
using Parstech.Shop.Context.Application.Dapper.Product.Queries;
using Parstech.Shop.Context.Application.DTOs.Product;
using Parstech.Shop.Context.Application.DTOs.ProductStockPrice;
using Parstech.Shop.Context.Application.Features.ProductStockPrice.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.ProductStockPrice.Handlers.Queries;

public class DiscountProductListPagingQueryHandler : IRequestHandler<DiscountProductListPagingQueryReq, ProductDiscountPagingDto>
{
        
    private readonly IMapper _mapper;
    private readonly IProductQueries _productQueries;
    private readonly string _connectionString;

    public DiscountProductListPagingQueryHandler(
        IMapper mapper,
           
        IProductQueries productQueries,
        IConfiguration configuration)
    { 
        _mapper = mapper;
        _productQueries = productQueries;
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }
    public async Task<ProductDiscountPagingDto> Handle(DiscountProductListPagingQueryReq request, CancellationToken cancellationToken)
    {
        ProductDiscountPagingDto response = new();
        string serach = "";
        string categury = "";
        string section = "";
        string exist = "";
        string showInPanel = "";

        if (!string.IsNullOrEmpty(request.parameter.Filter))
        {
            serach = $" AND p.Id={request.parameter.Filter}";
        }
        if (request.parameter.Categury != 0)
        {
            categury = $"AND EXISTS (SELECT 1 FROM dbo.ProductCategury WHERE ProductId = ps.ProductId AND CateguryId = {request.parameter.Categury}) ";
        }
        if (request.parameter.SectionId != 0)
        {
            section = $"AND EXISTS (SELECT 1 FROM dbo.ProductStockPriceSection as s WHERE ProductStockPriceId = ps.Id and s.SectionId={request.parameter.SectionId} )";
        }
        if (request.parameter.RepId != 0)
        {
            section = $"and ps.RepId={request.parameter.RepId}";
        }
        if (request.parameter.ShowInDiscountPanel !=null)
        {
            showInPanel = $"ps.ShowInDiscountPanels=1";
        }
        if (request.parameter.Exist != null)
        {
            switch (request.parameter.Exist)
            {
                case "Exist":
                    exist = $"AND ps.Quantity!=0";
                    break;
                case "NotExist":
                    exist = $"AND ps.Quantity=0";
                    break;
            }

        }


        int skip = (request.parameter.CurrentPage - 1) * request.parameter.TakePage;

        var query = $"SELECT ps.Id ,p.Id as ProductId,p.ParentId,p.Name,p.Code,p.TypeId,p.ShortLink,ps.ShowInDiscountPanels ,ps.SalePrice,ps.DiscountPrice,ps.DiscountDate, ps.StockStatus, ps.Quantity,ps.CateguryOfUserId FROM dbo.ProductStockPrice as ps INNER JOIN dbo.Product as p ON p.Id = ps.ProductId where ps.DiscountPrice!=0  and p.IsActive=1 {section} {categury} {exist} {serach} {showInPanel} and p.IsActive=1 ORDER BY CreateDate Desc OFFSET {skip} ROWS FETCH NEXT {request.parameter.TakePage} ROWS ONLY";
        var productReps = DapperHelper.ExecuteCommand<List<DapperProductDto>>(_connectionString, conn => conn.Query<DapperProductDto>(query).ToList());





        var productDtos = new List<ProductDto>();
        foreach (var item in productReps)
        {
            var Pdto = _mapper.Map<ProductDto>(item);
            Pdto.ProductStockPriceId = item.Id;
            productDtos.Add(Pdto);
            if (Pdto.TypeId == 2)
            {
                var variations = DapperHelper.ExecuteCommand<List<DapperProductDto>>(_connectionString, conn => conn.Query<DapperProductDto>(_productQueries.GetChildsForAdmin, new { productId = Pdto.ProductId }).ToList());
                foreach (var variation in variations)
                {
                    var vdto = _mapper.Map<ProductDto>(variation);
                    vdto.ProductStockPriceId = variation.Id;

                    productDtos.Add(vdto);
                }

            }

        }

        var AllListquery = $"SELECT ps.Id  FROM dbo.ProductStockPrice as ps INNER JOIN dbo.Product as p ON p.Id = ps.ProductId where ps.DiscountPrice!=0  and p.IsActive=1 {section} {categury} {exist} {serach} {showInPanel} and p.IsActive=1 ORDER BY CreateDate Desc OFFSET {skip} ROWS FETCH NEXT {request.parameter.TakePage} ROWS ONLY";

        var AllList = DapperHelper.ExecuteCommand<List<int>>(_connectionString, conn => conn.Query<int>(AllListquery).ToList());
        response.CurrentPage = request.parameter.CurrentPage;
        response.PageCount = AllList.Count() / request.parameter.TakePage;


        response.List = productDtos.ToArray();

        return response;

    }
}