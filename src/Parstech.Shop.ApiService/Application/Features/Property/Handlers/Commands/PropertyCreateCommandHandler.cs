using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Property.Requests.Commands;

namespace Parstech.Shop.ApiService.Application.Features.Property.Handlers.Commands;

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
        var Property = _mapper.Map<Shop.Domain.Models.Property>(request.PropertyDto);
        Domain.Models.Property? PropertyDto = await _propertyRep.AddAsync(Property);
        return _mapper.Map<PropertyDto>(Property);
    }
}