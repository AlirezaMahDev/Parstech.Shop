﻿using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Product.Handlers.Queries;

public class ProductEditContentQueryHandler : IRequestHandler<ProductEditContentQueryReq, ProductDto>
{
    private readonly IProductRepository _productRep;
    private readonly IMapper _mapper;

    public ProductEditContentQueryHandler(IProductRepository productRep, IMapper mapper)
    {
        _productRep = productRep;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(ProductEditContentQueryReq request, CancellationToken cancellationToken)
    {
        Shared.Models.Product? product = await _productRep.GetAsync(request.productId);
        product.Description = request.content;
        Shared.Models.Product result = await _productRep.UpdateAsync(product);
        return _mapper.Map<ProductDto>(result);
    }
}