using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.ProductCategury;
using Parstech.Shop.Context.Application.Features.ProductCategury.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.ProductCategury.Handlers.Commands;

public class ProductCateguryReadCommandHandler : IRequestHandler<ProductCateguryReadCommandReq, ProductCateguryDto>
{
    private readonly IProductCateguryRepository _productCateguryRep;
    private readonly IMapper _mapper;

    public ProductCateguryReadCommandHandler(IProductCateguryRepository productCateguryRep, IMapper mapper)
    {
        _productCateguryRep = productCateguryRep;
        _mapper = mapper;
    }
    public async Task<ProductCateguryDto> Handle(ProductCateguryReadCommandReq request, CancellationToken cancellationToken)
    {
        var pcategury =await _productCateguryRep.GetAsync(request.id);
        return _mapper.Map<ProductCateguryDto>(pcategury);
    }
}