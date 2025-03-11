using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Categury;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.PropertyCategury;
using Shop.Application.Features.Categury.Requests.Commands;
using Shop.Application.Features.PropertyCategury.Requests.Commands;

namespace Shop.Application.Features.Categury.Handlers.Commands
{
    public class PropertyCateguryUpdateCommandHandler : IRequestHandler<PropertyCateguryUpdateCommandReq, PropertyCateguryDto>
    {
        private readonly IPropertyCateguryRepository _propertyCatRep;
        private readonly IMapper _mapper;

        public PropertyCateguryUpdateCommandHandler(IPropertyCateguryRepository propertyCatRep, IMapper mapper)
        {
            _propertyCatRep = propertyCatRep;
            _mapper = mapper;
        }
        public async Task<PropertyCateguryDto> Handle(PropertyCateguryUpdateCommandReq request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<Domain.Models.PropertyCategury>(request.PropertyCateguryDto);
            var itemResult = await _propertyCatRep.UpdateAsync(item);
            return _mapper.Map<PropertyCateguryDto>(itemResult);
        }
    }
}
