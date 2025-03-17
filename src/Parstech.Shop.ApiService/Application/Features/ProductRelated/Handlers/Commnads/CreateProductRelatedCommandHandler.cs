using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.ProductRelated.Requests.Commnads;

namespace Parstech.Shop.ApiService.Application.Features.ProductRelated.Handlers.Commnads;

public class CreateProductRelatedCommandHandler : IRequestHandler<CreateProductRelatedCommandReq>
{
    private readonly IProductRelatedRepository _productRelatedRep;
    private readonly IProductRepository _productRep;
    private readonly IMapper _mapper;

    public CreateProductRelatedCommandHandler(IProductRelatedRepository productRelatedRep,
        IProductRepository productRep,
        IMapper mapper)
    {
        _productRelatedRep = productRelatedRep;
        _productRep = productRep;
        _mapper = mapper;
    }

    public async Task Handle(CreateProductRelatedCommandReq request, CancellationToken cancellationToken)
    {
        Shared.Models.ProductRelated? producrRelated =
            _mapper.Map<Shared.Models.ProductRelated>(request.productRelatedDto);
        await _productRelatedRep.AddAsync(producrRelated);
    }
}