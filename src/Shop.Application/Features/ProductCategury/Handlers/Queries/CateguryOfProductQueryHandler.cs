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
using Shop.Application.Features.ProductGallery.Requests.Queries;
using Shop.Application.Features.ProductProperty.Requests.Queries;

namespace Shop.Application.Features.ProductCategury.Handlers.Queries
{
    public class CateguryOfProductQueryHandler : IRequestHandler<CateguryOfProductQueryReq, ProductCateguryDto>
    {
        private readonly IProductCateguryRepository _productCateguryRep;
        private readonly IMapper _mapper;

        public CateguryOfProductQueryHandler(IProductCateguryRepository productCateguryRep, IMapper mapper)
        {
            _productCateguryRep = productCateguryRep;
            _mapper = mapper;
        }
        public async Task<ProductCateguryDto> Handle(CateguryOfProductQueryReq request, CancellationToken cancellationToken)
        {
            var pcategury =await _productCateguryRep.GetCateguryByProduct(request.productId);
            return _mapper.Map<ProductCateguryDto>(pcategury);
        }
    }
}
