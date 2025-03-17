using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.ProductCategury.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductCategury.Handlers.Commands;

public class ProductCateguryReadCommandHandler : IRequestHandler<ProductCateguryReadCommandReq, ProductCateguryDto>
{
    private readonly IProductCateguryRepository _productCateguryRep;
    private readonly IMapper _mapper;

    public ProductCateguryReadCommandHandler(IProductCateguryRepository productCateguryRep, IMapper mapper)
    {
        _productCateguryRep = productCateguryRep;
        _mapper = mapper;
    }

    public async Task<ProductCateguryDto> Handle(ProductCateguryReadCommandReq request,
        CancellationToken cancellationToken)
    {
        Shared.Models.ProductCategury? pcategury = await _productCateguryRep.GetAsync(request.id);
        return _mapper.Map<ProductCateguryDto>(pcategury);
    }
}