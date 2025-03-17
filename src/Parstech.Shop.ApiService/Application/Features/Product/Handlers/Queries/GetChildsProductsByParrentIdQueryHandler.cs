using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Product.Handlers.Queries;

public class
    GetChildsProductsByParrentIdQueryHandler : IRequestHandler<GetChildsProductsByParrentIdQueryReq, List<ProductDto>>
{
    private readonly IProductRepository _productRep;
    private readonly IProductStockPriceRepository _productStockRep;
    private readonly IMapper _mapper;

    public GetChildsProductsByParrentIdQueryHandler(IProductRepository productRep,
        IProductStockPriceRepository productStockRep,
        IMapper mapper)
    {
        _productRep = productRep;
        _productStockRep = productStockRep;
        _mapper = mapper;
    }

    public async Task<List<ProductDto>> Handle(GetChildsProductsByParrentIdQueryReq request,
        CancellationToken cancellationToken)
    {
        List<ProductDto> Result = new();

        List<Shared.Models.Product> list = await _productRep.GetProductsByParrentId(request.parrentId);
        foreach (Shared.Models.Product item in list)
        {
            ProductDto? dto = _mapper.Map<ProductDto>(item);
            int ps = await _productStockRep.GetFirstProductStockPriceIdFromProductId(item.Id);

            dto.ProductStockPriceId = ps;
            Result.Add(dto);
        }

        return Result;
    }
}