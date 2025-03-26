using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Property;
using Parstech.Shop.Context.Application.Features.Property.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.Property.Handlers.Queries;

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
        var list =await _propertyRep.GetPropertyBySearch(request.categuryId, request.PropertCateguriId,request.Filter);
        return _mapper.Map<List<PropertyDto>>(list);
    }
}