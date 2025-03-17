using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Property.Requests.Commands;

namespace Parstech.Shop.ApiService.Application.Features.Property.Handlers.Commands;

public class PropertyUpdateCommandHandler : IRequestHandler<PropertyUpdateCommandReq, PropertyDto>
{
    private readonly IPropertyRepository _propertyRep;
    private readonly IMapper _mapper;

    public PropertyUpdateCommandHandler(IPropertyRepository propertyRep, IMapper mapper)
    {
        _propertyRep = propertyRep;
        _mapper = mapper;
    }

    public async Task<PropertyDto> Handle(PropertyUpdateCommandReq request, CancellationToken cancellationToken)
    {
        var property = _mapper.Map<Shop.Domain.Models.Property>(request.PropertyDto);
        await _propertyRep.UpdateAsync(property);
        return _mapper.Map<PropertyDto>(property);
    }
}