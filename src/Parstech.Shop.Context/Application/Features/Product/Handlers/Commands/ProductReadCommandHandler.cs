using AutoMapper;

using Dapper;

using MediatR;
using Microsoft.Extensions.Configuration;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Dapper.Helper;
using Parstech.Shop.Context.Application.Dapper.Product.Queries;
using Parstech.Shop.Context.Application.DTOs.Product;
using Parstech.Shop.Context.Application.Features.Product.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.Product.Handlers.Commands;

public class ProductReadCommandHandler : IRequestHandler<ProductReadCommandReq, ProductDto>
{

    private IProductRepository _productRep;
    private IProductStockPriceRepository _productStockRep;
    private IProductGallleryRepository _productGalleryRep;
    private IMapper _mapper;
    private IMediator _madiiator;
    private IProductQueries _productQueries;
    private string _connectionString;

    public ProductReadCommandHandler(IProductRepository productRep, IProductStockPriceRepository productStockRep, IProductQueries productQueries,IConfiguration configuration, IProductGallleryRepository productGalleryRep, IMapper mapper, IMediator madiiator)
    {
        _productRep = productRep;
        _productGalleryRep = productGalleryRep;
        _mapper = mapper;
        _madiiator = madiiator;
        _productQueries= productQueries;
        _productStockRep = productStockRep;
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }


    public async Task<ProductDto> Handle(ProductReadCommandReq request, CancellationToken cancellationToken)
    {
        ProductDto result = new();
        //var product = await _productRep.GetAsync(request.id);
        var product =  DapperHelper.ExecuteCommand<DapperProductDto>(_connectionString, conn => conn.Query<DapperProductDto>(_productQueries.GetProductForAdmin, new { @productId = request.id }).FirstOrDefault());

        if (product != null)
        {
            result = _mapper.Map<ProductDto>(product);
            result.ProductStockPriceId = product.Id;
            var image = DapperHelper.ExecuteCommand<Domain.Models.ProductGallery>(_connectionString, conn => conn.Query<Domain.Models.ProductGallery>(_productQueries.GetMainImage, new { @productId = result.ProductId }).FirstOrDefault());
                
            if (image != null)
            {
                result.Image = image.ImageName;
            }
                
               
        }
        var firstStockId =await _productStockRep.GetFirstProductStockPriceIdFromProductId(result.Id);
        if (firstStockId != 0)
        {
            var ps = await _productStockRep.GetAsync(firstStockId);
            result.Quantity = ps.Quantity;
            result.DiscountPrice = ps.DiscountPrice;
            result.SalePrice = ps.SalePrice;
            result.ProductStockPriceId = firstStockId;
        }
           
        return result;
    }
}