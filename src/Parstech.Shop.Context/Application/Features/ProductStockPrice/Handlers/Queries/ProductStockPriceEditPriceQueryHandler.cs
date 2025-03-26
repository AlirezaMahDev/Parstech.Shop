using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.ProductStockPrice;
using Parstech.Shop.Context.Application.Features.ProductStockPrice.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.ProductStockPrice.Handlers.Queries;

public class ProductStockPriceEditPriceQueryHandler : IRequestHandler<ProductStockPriceEditPriceQueryReq, ProductStockPriceDto>
{
    private readonly IProductRepository _productRep;
    private readonly IProductStockPriceRepository _productStockRep;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ProductStockPriceEditPriceQueryHandler(IProductRepository productRep,
        IProductStockPriceRepository productStockRep,
        IMediator mediator, IMapper mapper)
    {
        _productRep = productRep;
        _mediator = mediator;
        _mapper = mapper;
        _productStockRep = productStockRep;
    }
    public async Task<ProductStockPriceDto> Handle(ProductStockPriceEditPriceQueryReq request, CancellationToken cancellationToken)
    {
            
        var product = await _productStockRep.GetAsync(request.product.ProductStockPriceId);
        product.Price = request.product.Price;
        product.BasePrice = request.product.BasePrice;
        product.DiscountPrice = request.product.DiscountPrice;
        product.SalePrice = request.product.SalePrice;
        var result = await _productStockRep.UpdateAsync(product);
        return _mapper.Map<ProductStockPriceDto>(result);
    }
}