using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Features.ProductCategury.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.ProductCategury.Handlers.Commands;

public class ProductCateguryDeleteCommandHandler : IRequestHandler<ProductCateguryDeleteCommandReq, int>
{
    private readonly IProductCateguryRepository _productCateguryRep;
    private readonly IMapper _mapper;

    public ProductCateguryDeleteCommandHandler(IProductCateguryRepository productCateguryRep, IMapper mapper)
    {
        _productCateguryRep = productCateguryRep;
        _mapper = mapper;
    }
    public async Task<int> Handle(ProductCateguryDeleteCommandReq request, CancellationToken cancellationToken)
    {
        var pcategury =await _productCateguryRep.GetAsync(request.id);
        var productId = pcategury.ProductId;
        await _productCateguryRep.DeleteAsync(pcategury);
        return productId;
    }
}