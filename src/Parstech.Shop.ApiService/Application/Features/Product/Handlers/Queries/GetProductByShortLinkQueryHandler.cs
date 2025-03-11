using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Product;
using Shop.Application.Features.Product.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Product.Handlers.Queries
{
    public class GetProductByShortLinkQueryHandler : IRequestHandler<GetProductByShortLinkQueryReq, ProductDto>
    {
        private readonly IProductRepository _productRep;
        private readonly IMapper _mapper;
        public GetProductByShortLinkQueryHandler(IProductRepository productRep,IMapper mapper)
        {
            _productRep = productRep;   
            _mapper = mapper;
        }
        public async Task<ProductDto> Handle(GetProductByShortLinkQueryReq request, CancellationToken cancellationToken)
        {
            var item =await _productRep.GetProductByShortLink(request.shortLink);
            if (item.TypeId==3)
            {
                var parrent =await _productRep.GetAsync(item.ParentId.Value);
                item = await _productRep.GetProductByShortLink(parrent.ShortLink);
            }
            return _mapper.Map<ProductDto>(item);
        }
    }
}
