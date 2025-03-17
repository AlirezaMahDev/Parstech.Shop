using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Product.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Product.Handlers.Commands;

public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommandReq, ProductDto>
{
    private readonly IProductRepository _productRep;
    private readonly IUserStoreRepository _userStoreRep;
    private readonly IMapper _mapper;
    private readonly IMediator _madiiator;
    private readonly IProductStockPriceRepository _productStockRep;

    public ProductUpdateCommandHandler(IProductRepository productRep,
        IUserStoreRepository userStoreRep,
        IMapper mapper,
        IMediator madiiator,
        IProductStockPriceRepository productStockRep)
    {
        _productRep = productRep;
        _userStoreRep = userStoreRep;
        _mapper = mapper;
        _madiiator = madiiator;
        _productStockRep = productStockRep;
    }

    public async Task<ProductDto> Handle(ProductUpdateCommandReq request, CancellationToken cancellationToken)
    {
        ProductDto result = new();
        Shared.Models.Product? product = _mapper.Map<Shared.Models.Product>(request.ProductDto);
        //var Store = await _userStoreRep.GetAsync(product.StoreId);
        //product.RepId = Store.RepId;

        if (product.TypeId != 5)
        {
            product.SingleSale = true;
        }

        if (product.TypeId == 1 || product.TypeId == 5)
        {
            List<Shared.Models.Product> childs = await _productRep.GetProductsByParrentId(product.Id);
            if (childs.Count > 0)
            {
                return result;
            }

            Shared.Models.Product productResult = await _productRep.UpdateAsync(product);

            return _mapper.Map<ProductDto>(productResult);
        }
        else
        {
            Shared.Models.Product productResult = await _productRep.UpdateAsync(product);

            return _mapper.Map<ProductDto>(productResult);
        }
    }
}