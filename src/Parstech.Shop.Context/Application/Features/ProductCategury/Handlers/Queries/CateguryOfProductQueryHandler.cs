using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.ProductCategury;
using Parstech.Shop.Context.Application.Features.ProductCategury.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.ProductCategury.Handlers.Queries;

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
        var pcategury =await _productCateguryRep.GetCateguryByProduct(request.productId);
        return _mapper.Map<ProductCateguryDto>(pcategury);
    }
}