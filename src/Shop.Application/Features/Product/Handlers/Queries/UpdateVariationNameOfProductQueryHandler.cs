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
    public class UpdateVariationNameOfProductQueryHandler : IRequestHandler<UpdateVariationNameOfProductQueryReq, bool>
    {
        private readonly IProductRepository _productRep;
        public UpdateVariationNameOfProductQueryHandler(IProductRepository productRep)
        {
            _productRep= productRep;
        }
        public async Task<bool> Handle(UpdateVariationNameOfProductQueryReq request, CancellationToken cancellationToken)
        {
            var product =await _productRep.GetAsync(request.productId);
            var parentProduct =await _productRep.GetAsync(product.ParentId.Value);
            product.VariationName=request.VariationName;
            product.Name = $"{parentProduct.Name}-{request.VariationName}";
            await _productRep.UpdateAsync(product);
            return true;
        }
    }
}
