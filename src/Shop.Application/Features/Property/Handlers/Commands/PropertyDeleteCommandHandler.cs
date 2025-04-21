using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Features.Property.Requests.Commands;

namespace Shop.Application.Features.Property.Handlers.Commands
{
    public class PropertyDeleteCommandHandler : IRequestHandler<PropertyDeleteCommandReq, Unit>
    {
        private readonly IPropertyRepository _propertyRep;
        private readonly IMapper _mapper;

        public PropertyDeleteCommandHandler(IPropertyRepository propertyRep, IMapper mapper)
        {
            _propertyRep = propertyRep;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(PropertyDeleteCommandReq request, CancellationToken cancellationToken)
        {
            var property = await _propertyRep.GetAsync(request.id);
            await _propertyRep.DeleteAsync(property);
            return Unit.Value;
        }
    }
}
