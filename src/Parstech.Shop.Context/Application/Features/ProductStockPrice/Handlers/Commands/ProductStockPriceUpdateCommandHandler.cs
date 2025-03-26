using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.ProductStockPrice;
using Parstech.Shop.Context.Application.Features.ProductStockPrice.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.ProductStockPrice.Handlers.Commands;

public class ProductStockPriceUpdateCommandHandler : IRequestHandler<ProductStockPriceUpdateCommandReq, ProductStockPriceDto>
{

    private IProductStockPriceRepository _productStockRep;
    private IProductRepository _productRep;
    private IUserStoreRepository _userStoreRep;
    private IMapper _mapper;
    private IMediator _madiiator;

    public ProductStockPriceUpdateCommandHandler(IProductStockPriceRepository productStockRep, IUserStoreRepository userStoreRep, IMapper mapper, IMediator madiiator, IProductRepository productRep)
    {
        _productStockRep = productStockRep;
        _userStoreRep = userStoreRep;
        _mapper = mapper;
        _madiiator = madiiator;
        _productRep = productRep;
    }

    public async Task<ProductStockPriceDto> Handle(ProductStockPriceUpdateCommandReq request, CancellationToken cancellationToken)
    {
        var productStock = _mapper.Map<Domain.Models.ProductStockPrice>(request.ProductStockPriceDto);
            
        var productResult = await _productStockRep.UpdateAsync(productStock);


        var product =await _productRep.GetAsync(productStock.ProductId);
        int pId;
        if (product.TypeId == 3)
        {
            pId = product.ParentId.Value;
        }
        else
        {
            pId = product.Id;
        }
        await _productRep.RefreshBestStockProduct(pId);

        return _mapper.Map<ProductStockPriceDto>(productResult);

    }
}