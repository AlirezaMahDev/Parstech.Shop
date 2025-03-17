using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.ProductCategury.Requests.Commands;

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
        Domain.Models.ProductCategury? pcategury =
            _mapper.Map<Domain.Models.ProductCategury>(request.ProductCateguryDto);
        Domain.Models.ProductCategury? result = await _productCateguryRep.UpdateAsync(pcategury);
        return _mapper.Map<ProductCateguryDto>(result);
    }
}