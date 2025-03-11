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
    public class ProductPropertyUpdateCommandHandler : IRequestHandler<ProductPropertyUpdateCommandReq, ProductPropertyDto>
    {
        private readonly IProductPropertyRepository _productPropertyRep;
        private readonly IMapper _mapper;

        public ProductPropertyUpdateCommandHandler(IProductPropertyRepository productPropertyRep, IMapper mapper)
        {
            _productPropertyRep = productPropertyRep;
            _mapper = mapper;
        }
        public async Task<ProductPropertyDto> Handle(ProductPropertyUpdateCommandReq request, CancellationToken cancellationToken)
        {
            var pproperty = _mapper.Map<Domain.Models.ProductProperty>(request.ProductPropertyDto);
           var result=await _productPropertyRep.UpdateAsync(pproperty);
           return _mapper.Map<ProductPropertyDto>(result);
        }
    }
}
