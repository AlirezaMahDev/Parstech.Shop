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
using Shop.Application.DTOs.ProductGallery;
using Shop.Application.DTOs.PropertyCategury;
using Shop.Application.Features.Categury.Requests.Commands;
using Shop.Application.Features.ProductGallery.Requests.Commands;
using Shop.Application.Features.PropertyCategury.Requests.Commands;

namespace Shop.Application.Features.PropertyCategury.Handlers.Commands
{
    public class PropertyCateguryCreateCommandHandler : IRequestHandler<PropertyCateguryCreateCommandReq, PropertyCateguryDto>
    {
        private readonly IPropertyCateguryRepository _propertyCatRep;
        private readonly IMapper _mapper;

        public PropertyCateguryCreateCommandHandler(IPropertyCateguryRepository propertyCatRep, IMapper mapper)
        {
            _propertyCatRep = propertyCatRep;
            _mapper = mapper;
        }
        public async Task<PropertyCateguryDto> Handle(PropertyCateguryCreateCommandReq request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<Domain.Models.PropertyCategury>(request.PropertyCateguryDto);

            var itemResult = await _propertyCatRep.AddAsync(item);

            return _mapper.Map<PropertyCateguryDto>(itemResult);
        }
    }
}
