using AutoMapper;

using Dapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.Dapper.Product.Queries;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.Product.Handlers.Queries;

public class SearchProductQueryHandler : IRequestHandler<SearchProductQueryReq, List<ProductDto>>
{
    private readonly IProductRepository _productRep;
    private readonly IProductGallleryRepository _productGallleryRep;
    private readonly IMapper _mapper;
    private IProductQueries _productQueries;
    private string _connectionString;
    private IProductStockPriceRepository _productStockPriceRep;

    public SearchProductQueryHandler(IProductRepository productRep,
        IMapper mapper,
        IProductQueries productQueries,
        IConfiguration configuration,
        IProductGallleryRepository productGallleryRep,
        IProductStockPriceRepository productStockPriceRep)
    {
        _productRep = productRep;
        _mapper = mapper;
        _productGallleryRep = productGallleryRep;
        _productQueries = productQueries;
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
        _productStockPriceRep = productStockPriceRep;
    }

    public async Task<List<ProductDto>> Handle(SearchProductQueryReq request, CancellationToken cancellationToken)
    {
        List<ProductDto>? Result = new();
        //var list =await _productRep.SearchProducts(request.Filter,request.Take);

        List<DapperProductDto> list = DapperHelper.ExecuteCommand(_connectionString,
            conn => conn.Query<DapperProductDto>(_productQueries.GetAllList).ToList());

        if (!string.IsNullOrEmpty(request.Filter))
        {
            List<DapperProductDto> searched = list.Where(p =>
                    p.Name.Contains(request.Filter, StringComparison.InvariantCultureIgnoreCase) ||
                    p.Code == request.Filter
                )
                .ToList();


            foreach (DapperProductDto item in searched.Take(request.Take))
            {
                int id = 0;
                if (item.ParentId != null) { id = item.ParentId.Value; }
                else { id = item.Id; }

                ProductDto? dto = _mapper.Map<ProductDto>(item);
                Domain.Models.ProductGallery image = DapperHelper.ExecuteCommand<Domain.Models.ProductGallery>(
                    _connectionString,
                    conn => conn
                        .Query<Domain.Models.ProductGallery>(_productQueries.GetMainImage, new { @productId = id })
                        .FirstOrDefault());

                if (image != null)
                {
                    dto.Image = image.ImageName;
                }

                int pstockId = await _productStockPriceRep.GetFirstProductStockPriceIdFromProductId(item.Id);
                dto.ProductStockPriceId = pstockId;
                //var pic = await _productGallleryRep.GetMainImageOfProduct(item.Id);
                //dto.Image = pic.ImageName;
                Result.Add(dto);
            }
        }


        return Result;
    }
}