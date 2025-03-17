using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.ProductCategury.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductCategury.Handlers.Queries;

public class CateguryOfProductQueryHandler : IRequestHandler<CateguryOfProductQueryReq, ProductCateguryDto>
{
    private readonly IProductCateguryRepository _productCateguryRep;
    private readonly IMapper _mapper;

    public CateguryOfProductQueryHandler(IProductCateguryRepository productCateguryRep, IMapper mapper)
    {
        _productCateguryRep = productCateguryRep;
        _mapper = mapper;
    }

    public async Task<ProductCateguryDto> Handle(CateguryOfProductQueryReq request, CancellationToken cancellationToken)
    {
        Shared.Models.ProductCategury? pcategury = await _productCateguryRep.GetCateguryByProduct(request.productId);
        return _mapper.Map<ProductCateguryDto>(pcategury);
    }
}