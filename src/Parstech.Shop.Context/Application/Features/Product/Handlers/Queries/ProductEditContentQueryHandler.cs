using AutoMapper;

using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Product;
using Parstech.Shop.Context.Application.Features.Product.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.Product.Handlers.Queries;

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