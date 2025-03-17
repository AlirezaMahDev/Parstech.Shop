using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Product.Handlers.Queries;

public class
    GetAllParentBundleProductQueryHanlder : IRequestHandler<GetAllParentBundleProductQueryReq, List<ProductSelectDto>>
{
    private readonly IProductRepository _productRep;
    private readonly IMapper _mapper;

    public GetAllParentBundleProductQueryHanlder(IProductRepository productRep, IMapper mapper)
    {
        _productRep = productRep;
        _mapper = mapper;
    }

    public async Task<List<ProductSelectDto>> Handle(GetAllParentBundleProductQueryReq request,
        CancellationToken cancellationToken)
    {
        List<Shared.Models.Product> products = await _productRep.GetAllParentBundleProduct(request.filter);
        List<ProductSelectDto> result = new();
        foreach (Shared.Models.Product product in products)
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