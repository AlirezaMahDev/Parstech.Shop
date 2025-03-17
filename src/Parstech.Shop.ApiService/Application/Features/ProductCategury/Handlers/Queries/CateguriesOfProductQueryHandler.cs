using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.ProductCategury.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.ProductCategury.Handlers.Queries;

public class CateguriesOfProductQueryHandler : IRequestHandler<CateguriesOfProductQueryReq, List<ProductCateguryDto>>
{
    private readonly IProductCateguryRepository _productCateguryRep;
    private readonly ICateguryRepository _categuryRep;
    private readonly IMapper _mapper;

    public CateguriesOfProductQueryHandler(IProductCateguryRepository productCateguryRep,
        ICateguryRepository categuryRep,
        IMapper mapper)
    {
        _productCateguryRep = productCateguryRep;
        _categuryRep = categuryRep;
        _mapper = mapper;
    }

    public async Task<List<ProductCateguryDto>> Handle(CateguriesOfProductQueryReq request,
        CancellationToken cancellationToken)
    {
        List<Domain.Models.ProductCategury>? result =
            await _productCateguryRep.GetCateguriesByProduct(request.productId);
        List<ProductCateguryDto>? FinalResult = new();
        foreach (Domain.Models.ProductCategury item in result)
        {
            Domain.Models.Categury? cat = await _categuryRep.GetAsync(item.CateguryId);
            ProductCateguryDto? ProductCateguryDto = _mapper.Map<ProductCateguryDto>(item);
            ProductCateguryDto.GroupTitle = cat.GroupTitle;
            ProductCateguryDto.IsParnet = cat.IsParnet;
            ProductCateguryDto.ParentId = cat.ParentId;
            FinalResult.Add(ProductCateguryDto);
        }

        return FinalResult;
    }
}