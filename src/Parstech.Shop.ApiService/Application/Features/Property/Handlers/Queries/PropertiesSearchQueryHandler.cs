using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Property.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.Property.Handlers.Queries;

public class PropertiesSearchQueryHandler : IRequestHandler<PropertiesSearchQueryReq, List<PropertyDto>>
{
    private readonly IPropertyRepository _propertyRep;
    private readonly IMapper _mapper;

    public PropertiesSearchQueryHandler(IPropertyRepository propertyRep, IMapper mapper)
    {
        _propertyRep = propertyRep;
        _mapper = mapper;
    }

    public async Task<List<PropertyDto>> Handle(PropertiesSearchQueryReq request, CancellationToken cancellationToken)
    {
        List<Domain.Models.Property>? list =
            await _propertyRep.GetPropertyBySearch(request.categuryId, request.PropertCateguriId, request.Filter);
        return _mapper.Map<List<PropertyDto>>(list);
    }
}