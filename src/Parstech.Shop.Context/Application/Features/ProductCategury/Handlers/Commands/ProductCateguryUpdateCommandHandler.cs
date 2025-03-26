using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.ProductCategury;
using Parstech.Shop.Context.Application.Features.ProductCategury.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.ProductCategury.Handlers.Commands;

public class ProductCateguryUpdateCommandHandler : IRequestHandler<ProductCateguryUpdateCommandReq, ProductCateguryDto>
{
    private readonly IProductCateguryRepository _productCateguryRep;
    private readonly IMapper _mapper;

    public ProductCateguryUpdateCommandHandler(IProductCateguryRepository productCateguryRep, IMapper mapper)
    {
        _productCateguryRep = productCateguryRep;
        _mapper = mapper;
    }
    public async Task<ProductCateguryDto> Handle(ProductCateguryUpdateCommandReq request, CancellationToken cancellationToken)
    {
        var pcategury = _mapper.Map<Domain.Models.ProductCategury>(request.ProductCateguryDto);
        var result=await _productCateguryRep.UpdateAsync(pcategury);
        return _mapper.Map<ProductCateguryDto>(result);
    }
}