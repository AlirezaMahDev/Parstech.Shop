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
            var PropertyDto =await _propertyRep.AddAsync(Property);
            return _mapper.Map<PropertyDto>(Property);
        }
    }
}
