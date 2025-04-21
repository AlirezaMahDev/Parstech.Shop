using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Features.Product.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Product.Handlers.Queries
{
    public class ProductShortLinkGeneratorQueryHandler : IRequestHandler<ProductShortLinkGeneratorQueryReq, string>
    {
        private readonly IProductRepository _productRep;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductShortLinkGeneratorQueryHandler(IProductRepository productRep, IMediator mediator, IMapper mapper)
        {
            _productRep = productRep;
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<string> Handle(ProductShortLinkGeneratorQueryReq request, CancellationToken cancellationToken)
        {
            bool check = false;
            string shortLink = null;
            while (!check)
            {
                shortLink = Guid.NewGuid().ToString().Substring(0,8);
                check = await _productRep.ProductShortLinkExist(shortLink);
            }
            return shortLink;
        }
    }
}
