using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.PropertyCategury;
using Shop.Application.Features.PropertyCategury.Requests.Commands;

namespace Shop.Application.Features.PropertyCategury.Handlers.Commands
{
    public class PropertyCateguryReadCommandHandler : IRequestHandler<PropertyCateguryReadCommandReq,PropertyCateguryDto>
    {
        private readonly IPropertyCateguryRepository _propertyCateguryRep;
        private readonly IMapper _mapper;

        public PropertyCateguryReadCommandHandler(IPropertyCateguryRepository propertyCateguryRep, IMapper mapper)
        {
            _propertyCateguryRep = propertyCateguryRep;
            _mapper = mapper;
        }
        public async Task<PropertyCateguryDto> Handle(PropertyCateguryReadCommandReq request, CancellationToken cancellationToken)
        {
            var item =await _propertyCateguryRep.GetAsync(request.Id);
            return _mapper.Map<PropertyCateguryDto>(item);
        }
    }
    public class PropertyCateguryReadsCommandHandler : IRequestHandler<PropertyCateguryReadsCommandReq, List<PropertyCateguryDto>>
    {
        private readonly IPropertyCateguryRepository _propertyCateguryRep;
        private readonly IMapper _mapper;

        public PropertyCateguryReadsCommandHandler(IPropertyCateguryRepository propertyCateguryRep, IMapper mapper)
        {
            _propertyCateguryRep = propertyCateguryRep;
            _mapper = mapper;
        }
        public async Task<List<PropertyCateguryDto>> Handle(PropertyCateguryReadsCommandReq request, CancellationToken cancellationToken)
        {
            var list =await _propertyCateguryRep.GetAll();
            return _mapper.Map<List<PropertyCateguryDto>>(list);
        }
    }
}
