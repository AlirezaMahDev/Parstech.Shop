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
    public class ProductCateguryReadCommandHandler : IRequestHandler<ProductCateguryReadCommandReq, ProductCateguryDto>
    {
        private readonly IProductCateguryRepository _productCateguryRep;
        private readonly IMapper _mapper;

        public ProductCateguryReadCommandHandler(IProductCateguryRepository productCateguryRep, IMapper mapper)
        {
            _productCateguryRep = productCateguryRep;
            _mapper = mapper;
        }
        public async Task<ProductCateguryDto> Handle(ProductCateguryReadCommandReq request, CancellationToken cancellationToken)
        {
            var pcategury =await _productCateguryRep.GetAsync(request.id);
            return _mapper.Map<ProductCateguryDto>(pcategury);
        }
    }
}
