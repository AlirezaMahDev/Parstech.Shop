using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Product;
using Shop.Application.Features.Product.Requests.Queries;

namespace Shop.Application.Features.Product.Handlers.Queries
{
    public class ProductEditContentQueryHandler : IRequestHandler<ProductEditContentQueryReq, ProductDto>
    {

        private readonly IProductRepository _productRep;
        private readonly IMapper _mapper;

        public ProductEditContentQueryHandler(IProductRepository productRep, IMapper mapper)
        {
            _productRep = productRep;
            _mapper = mapper;
        }
        public async Task<ProductDto> Handle(ProductEditContentQueryReq request, CancellationToken cancellationToken)
        {
            var product =await _productRep.GetAsync(request.productId);
            product.Description = request.content;
            var result =await _productRep.UpdateAsync(product);
            return _mapper.Map<ProductDto>(result);

        }
    }
}
