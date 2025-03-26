using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Property;
using Parstech.Shop.Context.Application.Features.Property.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.Property.Handlers.Commands;

public class PropertyCreateCommandHandler : IRequestHandler<PropertyCreateCommandReq, PropertyDto>
{
    private readonly IPropertyRepository _propertyRep;
    private readonly IMapper _mapper;

    public PropertyCreateCommandHandler(IPropertyRepository propertyRep, IMapper mapper)
    {
        _propertyRep = propertyRep;
        _mapper = mapper;
    }
    public async Task<PropertyDto> Handle(PropertyCreateCommandReq request, CancellationToken cancellationToken)
    {
        var Property = _mapper.Map<Parstech.Shop.Context.Domain.Models.Property>(request.PropertyDto);
        var PropertyDto =await _propertyRep.AddAsync(Property);
        return _mapper.Map<PropertyDto>(Property);
    }
}