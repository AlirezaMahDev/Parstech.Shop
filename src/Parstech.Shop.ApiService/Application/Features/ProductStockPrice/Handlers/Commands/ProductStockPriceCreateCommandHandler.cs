using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.ProductStockPrice.Requests.Commands;

namespace Parstech.Shop.ApiService.Application.Features.ProductStockPrice.Handlers.Commands;

public class
    ProductStockPriceCreateCommandHandler : IRequestHandler<ProductStockPriceCreateCommandReq, ProductStockPriceDto>
{
    private IProductRepository _productRep;
    private IProductStockPriceRepository _productStockRep;
    private IUserStoreRepository _userStoreRep;
    private IMapper _mapper;
    private IMediator _mediator;

    public ProductStockPriceCreateCommandHandler(IProductRepository productRep,
        IUserStoreRepository userStoreRep,
        IMapper mapper,
        IMediator mediator,
        IProductStockPriceRepository productStockRep)
    {
        _productRep = productRep;
        _userStoreRep = userStoreRep;
        _mapper = mapper;
        _mediator = mediator;
        _productStockRep = productStockRep;
    }

    public async Task<ProductStockPriceDto> Handle(ProductStockPriceCreateCommandReq request,
        CancellationToken cancellationToken)
    {
        Domain.Models.ProductStockPrice? item =
            _mapper.Map<Domain.Models.ProductStockPrice>(request.ProductStockPriceDto);

        //var Store =await _userStoreRep.GetAsync(product.StoreId);
        item.Product = null;
        item.Rep = null;
        Domain.Models.ProductStockPrice? productResult = await _productStockRep.AddAsync(item);


        return _mapper.Map<ProductStockPriceDto>(productResult);
    }
}