﻿using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Features.ProductRelated.Requests.Commnads;

namespace Parstech.Shop.Context.Application.Features.ProductRelated.Handlers.Commnads;

public class CreateProductRelatedCommandHandler : IRequestHandler<CreateProductRelatedCommandReq>
{
    private readonly IProductRelatedRepository _productRelatedRep;
    private readonly IProductRepository _productRep;
    private readonly IMapper _mapper;
    public CreateProductRelatedCommandHandler(IProductRelatedRepository productRelatedRep,
        IProductRepository productRep, IMapper mapper)
    {
        _productRelatedRep = productRelatedRep;
        _productRep = productRep;
        _mapper = mapper;
    }
    public async Task Handle(CreateProductRelatedCommandReq request, CancellationToken cancellationToken)
    {
        var producrRelated = _mapper.Map<Domain.Models.ProductRelated>(request.productRelatedDto);
        await _productRelatedRep.AddAsync(producrRelated);
    }
}