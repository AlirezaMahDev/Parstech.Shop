using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Property;
using Parstech.Shop.Context.Application.Features.Property.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.Property.Handlers.Commands;

public class PropertyReadCommandHandler : IRequestHandler<PropertyReadCommandReq, PropertyDto>
{
    private readonly IPropertyRepository _propertyRep;
    private readonly IMapper _mapper;

    public PropertyReadCommandHandler(IPropertyRepository propertyRep, IMapper mapper)
    {
        _propertyRep = propertyRep;
        _mapper = mapper;
    }
    public async Task<PropertyDto> Handle(PropertyReadCommandReq request, CancellationToken cancellationToken)
    {
        var property =await _propertyRep.GetAsync(request.id);
        return _mapper.Map<PropertyDto>(property);
    }
}