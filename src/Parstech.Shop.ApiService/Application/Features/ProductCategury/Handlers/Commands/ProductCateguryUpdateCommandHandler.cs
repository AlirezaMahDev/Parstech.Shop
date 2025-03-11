using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.ProductCategury;
using Shop.Application.DTOs.ProductGallery;
using Shop.Application.DTOs.ProductProperty;
using Shop.Application.Features.ProductCategury.Requests.Commands;
using Shop.Application.Features.ProductGallery.Requests.Commands;
using Shop.Application.Features.ProductProperty.Requests.Commands;

namespace Shop.Application.Features.ProductCategury.Handlers.Commands
{
    public class ProductCateguryUpdateCommandHandler : IRequestHandler<ProductCateguryUpdateCommandReq, ProductCateguryDto>
    {
        private readonly IProductCateguryRepository _productCateguryRep;
        private readonly IMapper _mapper;

        public ProductCateguryUpdateCommandHandler(IProductCateguryRepository productCateguryRep, IMapper mapper)
        {
            _productCateguryRep = productCateguryRep;
            _mapper = mapper;
        }
        public async Task<ProductCateguryDto> Handle(ProductCateguryUpdateCommandReq request, CancellationToken cancellationToken)
        {
            var pcategury = _mapper.Map<Domain.Models.ProductCategury>(request.ProductCateguryDto);
           var result=await _productCateguryRep.UpdateAsync(pcategury);
           return _mapper.Map<ProductCateguryDto>(result);
        }
    }
}
