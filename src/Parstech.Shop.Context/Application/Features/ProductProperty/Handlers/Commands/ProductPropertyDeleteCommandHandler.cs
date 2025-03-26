using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Features.ProductProperty.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.ProductProperty.Handlers.Commands;

public class ProductPropertyDeleteCommandHandler : IRequestHandler<ProductPropertyDeleteCommandReq, Unit>
{
    private readonly IProductPropertyRepository _productPropertyRep;
    private readonly IMapper _mapper;

    public ProductPropertyDeleteCommandHandler(IProductPropertyRepository productPropertyRep, IMapper mapper)
    {
        _productPropertyRep = productPropertyRep;
        _mapper = mapper;
    }
    public async Task<Unit> Handle(ProductPropertyDeleteCommandReq request, CancellationToken cancellationToken)
    {
        var pproperty =await _productPropertyRep.GetAsync(request.id);
        await _productPropertyRep.DeleteAsync(pproperty);
        return Unit.Value;
    }
}