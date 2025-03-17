using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.ProductCategury.Requests.Commands;

namespace Parstech.Shop.ApiService.Application.Features.ProductCategury.Handlers.Commands;

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
        Shared.Models.ProductCategury? pcategury = await _productCateguryRep.GetAsync(request.id);
        int productId = pcategury.ProductId;
        await _productCateguryRep.DeleteAsync(pcategury);
        return productId;
    }
}