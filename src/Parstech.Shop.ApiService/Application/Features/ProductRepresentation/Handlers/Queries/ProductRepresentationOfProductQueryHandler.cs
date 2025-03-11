using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.ProductRepresentation;
using Shop.Application.Features.ProductRepresentation.Requests.Queries;

namespace Shop.Application.Features.ProductRepresentation.Handlers.Queries
{
    public class ProductRepresentationOfProductQueryHandler : IRequestHandler<ProductRepresentationOfProductQueryReq, ProductRepresentationDto>
    {
        private readonly IProductRepresesntationRepository _productRepresentationRep;
        private readonly IMapper _mapper;

        public ProductRepresentationOfProductQueryHandler(IProductRepresesntationRepository productRepresentationRep, IMapper mapper)
        {
            _productRepresentationRep = productRepresentationRep;
            _mapper = mapper;
        }
        public async Task<ProductRepresentationDto> Handle(ProductRepresentationOfProductQueryReq request, CancellationToken cancellationToken)
        {
            var productRep = await _productRepresentationRep.GetProductRepresentationOfProduct(request.productId);
            return _mapper.Map<ProductRepresentationDto>(productRep);
        }
    }
}
