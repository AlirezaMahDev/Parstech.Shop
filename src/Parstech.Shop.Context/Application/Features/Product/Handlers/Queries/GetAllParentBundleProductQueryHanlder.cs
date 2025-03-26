using AutoMapper;

using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Product;
using Parstech.Shop.Context.Application.Features.Product.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.Product.Handlers.Queries;

public class GetAllParentBundleProductQueryHanlder : IRequestHandler<GetAllParentBundleProductQueryReq, List<ProductSelectDto>>
{
    private readonly IProductRepository _productRep;
    private readonly IMapper _mapper;

    public GetAllParentBundleProductQueryHanlder(IProductRepository productRep, IMapper mapper)
    {
        _productRep = productRep;
        _mapper = mapper;
    }

    public async Task<List<ProductSelectDto>> Handle(GetAllParentBundleProductQueryReq request, CancellationToken cancellationToken)
    {
        var products = await _productRep.GetAllParentBundleProduct(request.filter);
        List<ProductSelectDto> result = new();
        foreach (var product in products)
        {
            var dto = new ProductSelectDto();
            dto.ProductName = product.Name;
            dto.Code = product.Code;
            dto.Id = product.Id;
            result.Add(dto);
        }
        return result;
    }
}