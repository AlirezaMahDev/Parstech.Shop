using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.Product.Handlers.Queries;

public class ChangeDatetimeProductsQueryHandler : IRequestHandler<ChangeDatetimeProductsQueryReq, Unit>
{
    private readonly IProductRepository _productRep;

    public ChangeDatetimeProductsQueryHandler(IProductRepository productRep)
    {
        _productRep = productRep;
    }

    public async Task<Unit> Handle(ChangeDatetimeProductsQueryReq request, CancellationToken cancellationToken)
    {
        IReadOnlyList<Shared.Models.Product> list = await _productRep.GetAll();
        foreach (Shared.Models.Product? item in list.OrderByDescending(u => u.CreateDate))
        {
            item.CreateDate = DateTime.Now;
            await _productRep.UpdateAsync(item);
        }

        return Unit.Value;
    }
}