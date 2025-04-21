using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.ProductProperty;
using Shop.Application.Features.ProductProperty.Requests.Commands;

namespace Shop.Application.Features.ProductProperty.Handlers.Commands
{
    public class ProductPropertyReadCommandHandler : IRequestHandler<ProductPropertyReadCommandReq, ProductPropertyDto>
    {
        private readonly IProductPropertyRepository _productPropertyRep;
        private readonly IMapper _mapper;

        public ProductPropertyReadCommandHandler(IProductPropertyRepository productPropertyRep, IMapper mapper)
        {
            _productPropertyRep = productPropertyRep;
            _mapper = mapper;
        }
        public async Task<ProductPropertyDto> Handle(ProductPropertyReadCommandReq request, CancellationToken cancellationToken)
        {
            var pproperty = await _productPropertyRep.GetAsync(request.id);
            return _mapper.Map<ProductPropertyDto>(pproperty);
        }
    }
}
