using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.ProductCategury.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductCategury.Handlers.Commands;

public class ProductCateguryUpdateCommandHandler : IRequestHandler<ProductCateguryUpdateCommandReq, ProductCateguryDto>
{
    private readonly IProductCateguryRepository _productCateguryRep;
    private readonly IMapper _mapper;

    public ProductCateguryUpdateCommandHandler(IProductCateguryRepository productCateguryRep, IMapper mapper)
    {
        _productCateguryRep = productCateguryRep;
        _mapper = mapper;
    }

    public async Task<ProductCateguryDto> Handle(ProductCateguryUpdateCommandReq request,
        CancellationToken cancellationToken)
    {
        Shared.Models.ProductCategury? pcategury =
            _mapper.Map<Shared.Models.ProductCategury>(request.ProductCateguryDto);
        Shared.Models.ProductCategury result = await _productCateguryRep.UpdateAsync(pcategury);
        return _mapper.Map<ProductCateguryDto>(result);
    }
}