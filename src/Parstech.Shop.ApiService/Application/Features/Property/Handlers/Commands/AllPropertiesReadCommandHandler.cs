using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Property.Requests.Commands;

namespace Parstech.Shop.ApiService.Application.Features.Property.Handlers.Commands;

public class AllPropertiesReadCommandHandler : IRequestHandler<AllPropertiesReadCommandReq, List<PropertyDto>>
{
    private readonly IPropertyRepository _propertyRep;
    private readonly IMapper _mapper;

    public AllPropertiesReadCommandHandler(IPropertyRepository propertyRep, IMapper mapper)
    {
        _propertyRep = propertyRep;
        _mapper = mapper;
    }

    public async Task<List<PropertyDto>> Handle(AllPropertiesReadCommandReq request,
        CancellationToken cancellationToken)
    {
        IReadOnlyList<Domain.Models.Property>? properties = await _propertyRep.GetAll();
        List<PropertyDto> result = new();
        foreach (Domain.Models.Property property in properties)
        {
            result.Add(_mapper.Map<PropertyDto>(property));
        }

        return result;
    }
}