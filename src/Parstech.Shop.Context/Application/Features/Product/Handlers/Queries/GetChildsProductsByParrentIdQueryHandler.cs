using AutoMapper;

using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Product;
using Parstech.Shop.Context.Application.Features.Product.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.Product.Handlers.Queries;

public class GetChildsProductsByParrentIdQueryHandler : IRequestHandler<GetChildsProductsByParrentIdQueryReq, List<ProductDto>>
{
    private readonly IProductRepository _productRep;
    private readonly IProductStockPriceRepository _productStockRep;
    private readonly IMapper _mapper;
    public GetChildsProductsByParrentIdQueryHandler(IProductRepository productRep, IProductStockPriceRepository productStockRep, IMapper mapper)
    {
        _productRep = productRep;
        _productStockRep = productStockRep;
        _mapper = mapper;
    }
    public async Task<List<ProductDto>> Handle(GetChildsProductsByParrentIdQueryReq request, CancellationToken cancellationToken)
    { 
        List<ProductDto> Result = new();
        
        var list =await _productRep.GetProductsByParrentId(request.parrentId);
        foreach (var item in list)
        {
            var dto = _mapper.Map<ProductDto>(item);
            var ps =await _productStockRep.GetFirstProductStockPriceIdFromProductId(item.Id);
               
            dto.ProductStockPriceId = ps;
            Result.Add(dto);
        }
        return Result;
    }
}