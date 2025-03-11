using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Features.ProductRelated.Requests.Commnads;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.ProductRelated.Handlers.Commnads
{
    public class CreateProductRelatedCommandHandler : IRequestHandler<CreateProductRelatedCommandReq>
    {
        private readonly IProductRelatedRepository _productRelatedRep;
        private readonly IProductRepository _productRep;
        private readonly IMapper _mapper;
        public CreateProductRelatedCommandHandler(IProductRelatedRepository productRelatedRep,
            IProductRepository productRep, IMapper mapper)
        {
            _productRelatedRep = productRelatedRep;
            _productRep = productRep;
            _mapper = mapper;
        }
        public async Task Handle(CreateProductRelatedCommandReq request, CancellationToken cancellationToken)
        {
            var producrRelated = _mapper.Map<Domain.Models.ProductRelated>(request.productRelatedDto);
            await _productRelatedRep.AddAsync(producrRelated);
        }
    }
}
