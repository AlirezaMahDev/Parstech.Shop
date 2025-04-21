using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Features.ProductProperty.Requests.Commands;

namespace Shop.Application.Features.ProductProperty.Handlers.Commands
{
    public class ProductPropertyDeleteCommandHandler : IRequestHandler<ProductPropertyDeleteCommandReq, Unit>
    {
        private readonly IProductPropertyRepository _productPropertyRep;
        private readonly IMapper _mapper;

        public ProductPropertyDeleteCommandHandler(IProductPropertyRepository productPropertyRep, IMapper mapper)
        {
            _productPropertyRep = productPropertyRep;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(ProductPropertyDeleteCommandReq request, CancellationToken cancellationToken)
        {
            var pproperty =await _productPropertyRep.GetAsync(request.id);
           await _productPropertyRep.DeleteAsync(pproperty);
           return Unit.Value;
        }
    }
}
