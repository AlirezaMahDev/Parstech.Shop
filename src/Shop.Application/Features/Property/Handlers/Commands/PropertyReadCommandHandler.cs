using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Property;
using Shop.Application.Features.Property.Requests.Commands;

namespace Shop.Application.Features.Property.Handlers.Commands
{
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
}
