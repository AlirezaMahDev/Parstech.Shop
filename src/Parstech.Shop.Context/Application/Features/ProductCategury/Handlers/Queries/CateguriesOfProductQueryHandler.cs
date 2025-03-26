using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.ProductCategury;
using Parstech.Shop.Context.Application.Features.ProductCategury.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.ProductCategury.Handlers.Queries;

public class CateguriesOfProductQueryHandler : IRequestHandler<CateguriesOfProductQueryReq, List<ProductCateguryDto>>
{
    private readonly IProductCateguryRepository _productCateguryRep;
    private readonly ICateguryRepository _categuryRep;
    private readonly IMapper _mapper;

    public CateguriesOfProductQueryHandler(IProductCateguryRepository productCateguryRep, ICateguryRepository categuryRep, IMapper mapper)
    {
        _productCateguryRep = productCateguryRep;
        _categuryRep = categuryRep;
        _mapper = mapper;
    }
    public async Task<List<ProductCateguryDto>> Handle(CateguriesOfProductQueryReq request, CancellationToken cancellationToken)
    {
        var result =await _productCateguryRep.GetCateguriesByProduct(request.productId);
        var FinalResult =new List<ProductCateguryDto>();
        foreach (var item in result)
        {
            var cat = await _categuryRep.GetAsync(item.CateguryId);
            var ProductCateguryDto=_mapper.Map<ProductCateguryDto>(item);
            ProductCateguryDto.GroupTitle =cat.GroupTitle;
            ProductCateguryDto.IsParnet = cat.IsParnet;
            ProductCateguryDto.ParentId = cat.ParentId;
            FinalResult.Add(ProductCateguryDto);
        }
        return FinalResult;
    }
}