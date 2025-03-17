using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Product.Handlers.Queries;

public class ProductQuickEditQueryHandler : IRequestHandler<ProductQuickEditQueryReq, ProductDto>
{
    private readonly IProductRepository _productRep;
    private readonly IUserStoreRepository _userStoreRep;
    private readonly IProductStockPriceRepository _productStockPriceRep;
    private readonly IMapper _mapper;

    public ProductQuickEditQueryHandler(IProductRepository productRep,
        IUserStoreRepository userStoreRep,
        IProductStockPriceRepository productStockPriceRep,
        IMapper mapper)
    {
        _productRep = productRep;
        _userStoreRep = userStoreRep;
        _productStockPriceRep = productStockPriceRep;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(ProductQuickEditQueryReq request, CancellationToken cancellationToken)
    {
        Shared.Models.Product? product = await _productRep.GetAsync(request.ProductQuickEditDto.ProductId);
        product.Name = request.ProductQuickEditDto.Name;
        product.LatinName = request.ProductQuickEditDto.LatinName;
        product.Code = request.ProductQuickEditDto.Code;
        product.TaxId = request.ProductQuickEditDto.TaxId;
        product.BrandId = request.ProductQuickEditDto.BrandId;
        product.TypeId = request.ProductQuickEditDto.TypeId;
        product.ParentId = request.ProductQuickEditDto.ParentId;
        product.Score = request.ProductQuickEditDto.Score;
        product.ParentId = request.ProductQuickEditDto.ParentId;
        Shared.Models.Product result = await _productRep.UpdateAsync(product);


        Shared.Models.ProductStockPrice? prosuctStock =
            await _productStockPriceRep.GetAsync(request.ProductQuickEditDto.Id);
        Shared.Models.UserStore? store = await _userStoreRep.GetAsync(request.ProductQuickEditDto.StoreId);
        prosuctStock.StoreId = store.Id;
        prosuctStock.QuantityPerBundle = request.ProductQuickEditDto.QuantityPerBundle;
        prosuctStock.RepId = store.RepId;

        await _productStockPriceRep.UpdateAsync(prosuctStock);
        return _mapper.Map<ProductDto>(result);
    }
}