using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.Product.Handlers.Queries;

public class UpdateVariationNameOfProductQueryHandler : IRequestHandler<UpdateVariationNameOfProductQueryReq, bool>
{
    private readonly IProductRepository _productRep;

    public UpdateVariationNameOfProductQueryHandler(IProductRepository productRep)
    {
        _productRep = productRep;
    }

    public async Task<bool> Handle(UpdateVariationNameOfProductQueryReq request, CancellationToken cancellationToken)
    {
        Domain.Models.Product? product = await _productRep.GetAsync(request.productId);
        Domain.Models.Product? parentProduct = await _productRep.GetAsync(product.ParentId.Value);
        product.VariationName = request.VariationName;
        product.Name = $"{parentProduct.Name}-{request.VariationName}";
        await _productRep.UpdateAsync(product);
        return true;
    }
}