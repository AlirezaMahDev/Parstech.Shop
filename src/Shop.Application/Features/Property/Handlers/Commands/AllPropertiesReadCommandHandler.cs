using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Property;
using Shop.Application.Features.Property.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Property.Handlers.Commands
{
    public class AllPropertiesReadCommandHandler : IRequestHandler<AllPropertiesReadCommandReq, List<PropertyDto>>
    {
        private readonly IPropertyRepository _propertyRep;
        private readonly IMapper _mapper;

        public AllPropertiesReadCommandHandler(IPropertyRepository propertyRep, IMapper mapper)
        {
            _propertyRep = propertyRep;
            _mapper = mapper;
        }

        public async Task<List<PropertyDto>> Handle(AllPropertiesReadCommandReq request, CancellationToken cancellationToken)
        {
            var properties = await _propertyRep.GetAll();
            List<PropertyDto> result = new List<PropertyDto>();
            foreach (var property in properties)
            {
                result.Add(_mapper.Map<PropertyDto>(property));
            }
            return result;
        }
    }
}
