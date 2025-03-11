using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Property;
using Shop.Application.Features.Property.Requests.Queries;

namespace Shop.Application.Features.Property.Handlers.Queries
{
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
}
