﻿using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.ProductStockPrice.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductStockPrice.Handlers.Commands;

public class
    ProductStockPriceUpdateCommandHandler : IRequestHandler<ProductStockPriceUpdateCommandReq, ProductStockPriceDto>
{
    private IProductStockPriceRepository _productStockRep;
    private IProductRepository _productRep;
    private IUserStoreRepository _userStoreRep;
    private IMapper _mapper;
    private IMediator _madiiator;

    public ProductStockPriceUpdateCommandHandler(IProductStockPriceRepository productStockRep,
        IUserStoreRepository userStoreRep,
        IMapper mapper,
        IMediator madiiator,
        IProductRepository productRep)
    {
        _productStockRep = productStockRep;
        _userStoreRep = userStoreRep;
        _mapper = mapper;
        _madiiator = madiiator;
        _productRep = productRep;
    }

    public async Task<ProductStockPriceDto> Handle(ProductStockPriceUpdateCommandReq request,
        CancellationToken cancellationToken)
    {
        Shared.Models.ProductStockPrice? productStock =
            _mapper.Map<Shared.Models.ProductStockPrice>(request.ProductStockPriceDto);

        Shared.Models.ProductStockPrice productResult = await _productStockRep.UpdateAsync(productStock);


        Shared.Models.Product? product = await _productRep.GetAsync(productStock.ProductId);
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