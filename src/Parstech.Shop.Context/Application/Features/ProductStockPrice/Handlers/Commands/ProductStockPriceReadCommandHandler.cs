using AutoMapper;
using Dapper;

using MediatR;
using Microsoft.Extensions.Configuration;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Convertor;
using Parstech.Shop.Context.Application.Dapper.Helper;
using Parstech.Shop.Context.Application.DTOs.ProductStockPrice;
using Parstech.Shop.Context.Application.Features.ProductStockPrice.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.ProductStockPrice.Handlers.Commands;

public class ProductStockPriceReadCommandHandler : IRequestHandler<ProductStockPriceReadCommandReq, ProductStockPriceDto>
{

    private IProductStockPriceRepository _productStockRep;
    private readonly string _connectionString;
    private IMapper _mapper;
    private IMediator _madiiator;

    public ProductStockPriceReadCommandHandler(IProductStockPriceRepository productStockRep,
        IMapper mapper,
        IMediator madiiator,IConfiguration configuration)
    {
        _productStockRep= productStockRep;
        _mapper = mapper;
        _madiiator = madiiator;
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }


    public async Task<ProductStockPriceDto> Handle(ProductStockPriceReadCommandReq request, CancellationToken cancellationToken)
    {
        ProductStockPriceDto result = new();
        //var product = await _productRep.GetAsync(request.id);
        //var product = await _productStockRep.GetAsync(request.id);

        var query = $"Select* From ProductStockPrice Where Id={request.id}";
        var product = DapperHelper.ExecuteCommand<ProductStockPriceDto>(_connectionString, conn => conn.Query<ProductStockPriceDto>(query).FirstOrDefault());


        result = _mapper.Map<ProductStockPriceDto>(product);
        result.TextPrice = product.Price.ToString();
        result.TextSalePrice = product.SalePrice.ToString();
        result.TextDiscountPrice = product.DiscountPrice.ToString();
        result.TextBasePrice = product.BasePrice.ToString();
        if(product.DiscountDate != null)
        {
            result.DiscountDateShamsi = product.DiscountDate.Value.ToShamsi();
        }
            
        return result;
    }
}