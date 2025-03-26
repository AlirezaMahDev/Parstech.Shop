using AutoMapper;

using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Product;
using Parstech.Shop.Context.Application.Features.Product.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.Product.Handlers.Queries;

public class GetAllParentVariableProductQueryHandler : IRequestHandler<GetAllParentVariableProductQueryReq, List<ProductSelectDto>>
{
    private readonly IProductRepository _productRep;
    private readonly IMapper _mapper;

    public GetAllParentVariableProductQueryHandler(IProductRepository productRep, IMapper mapper)
    {
        _productRep = productRep;
        _mapper = mapper;
    }

    public async Task<List<ProductSelectDto>> Handle(GetAllParentVariableProductQueryReq request, CancellationToken cancellationToken)
    {
        var products = await _productRep.GetAllParentVariableProduct(request.filter);
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