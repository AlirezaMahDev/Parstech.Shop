using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.Product.Handlers.Queries;

public class
    GetAllParentVariableProductQueryHandler : IRequestHandler<GetAllParentVariableProductQueryReq,
    List<ProductSelectDto>>
{
    private readonly IProductRepository _productRep;
    private readonly IMapper _mapper;

    public GetAllParentVariableProductQueryHandler(IProductRepository productRep, IMapper mapper)
    {
        _productRep = productRep;
        _mapper = mapper;
    }

    public async Task<List<ProductSelectDto>> Handle(GetAllParentVariableProductQueryReq request,
        CancellationToken cancellationToken)
    {
        List<Domain.Models.Product>? products = await _productRep.GetAllParentVariableProduct(request.filter);
        List<ProductSelectDto> result = new();
        foreach (Domain.Models.Product product in products)
        {
            ProductSelectDto dto = new();
            dto.ProductName = product.Name;
            dto.Code = product.Code;
            dto.Id = product.Id;
            result.Add(dto);
        }

        return result;
    }
}