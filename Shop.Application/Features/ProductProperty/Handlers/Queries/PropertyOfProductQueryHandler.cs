using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.ProductProperty;
using Shop.Application.Features.ProductProperty.Requests.Queries;

namespace Shop.Application.Features.ProductProperty.Handlers.Queries
{

    public class PropertyOfProductQueryHandler : IRequestHandler<PropertyOfProductQueryReq, ProductPropertyDto>
    {
        private readonly IProductPropertyRepository _productPropertyRep;
        private readonly IMapper _mapper;

        public PropertyOfProductQueryHandler(IProductPropertyRepository productPropertyRep, IMapper mapper)
        {
            _productPropertyRep = productPropertyRep;
            _mapper = mapper;
        }
        public async Task<ProductPropertyDto> Handle(PropertyOfProductQueryReq request, CancellationToken cancellationToken)
        {
            var pproperty =await _productPropertyRep.GetpropertyByProduct(request.productId);
            return _mapper.Map<ProductPropertyDto>(pproperty);
        }
    }
}
