using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Features.Property.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.Property.Handlers.Commands;

public class PropertyDeleteCommandHandler : IRequestHandler<PropertyDeleteCommandReq, Unit>
{
    private readonly IPropertyRepository _propertyRep;
    private readonly IMapper _mapper;

    public PropertyDeleteCommandHandler(IPropertyRepository propertyRep, IMapper mapper)
    {
        _propertyRep = propertyRep;
        _mapper = mapper;
    }
    public async Task<Unit> Handle(PropertyDeleteCommandReq request, CancellationToken cancellationToken)
    {
        var property = await _propertyRep.GetAsync(request.id);
        await _propertyRep.DeleteAsync(property);
        return Unit.Value;
    }
}