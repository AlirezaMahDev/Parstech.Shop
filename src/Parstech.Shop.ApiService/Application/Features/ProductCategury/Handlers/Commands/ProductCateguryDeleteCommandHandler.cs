using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Features.ProductCategury.Requests.Commands;
using Shop.Application.Features.ProductGallery.Requests.Commands;
using Shop.Application.Features.ProductProperty.Requests.Commands;

namespace Shop.Application.Features.ProductCategury.Handlers.Commands
{
    public class ProductCateguryDeleteCommandHandler : IRequestHandler<ProductCateguryDeleteCommandReq, int>
    {
        private readonly IProductCateguryRepository _productCateguryRep;
        private readonly IMapper _mapper;

        public ProductCateguryDeleteCommandHandler(IProductCateguryRepository productCateguryRep, IMapper mapper)
        {
            _productCateguryRep = productCateguryRep;
            _mapper = mapper;
        }
        public async Task<int> Handle(ProductCateguryDeleteCommandReq request, CancellationToken cancellationToken)
        {
            var pcategury =await _productCateguryRep.GetAsync(request.id);
            var productId = pcategury.ProductId;
            await _productCateguryRep.DeleteAsync(pcategury);
            return productId;
        }
    }
}
