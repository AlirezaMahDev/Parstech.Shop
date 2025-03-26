using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Features.Product.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.Product.Handlers.Queries;

public class ChangeDatetimeProductsQueryHandler : IRequestHandler<ChangeDatetimeProductsQueryReq, Unit>
{
    private readonly IProductRepository _productRep;
    public ChangeDatetimeProductsQueryHandler(IProductRepository productRep)
    {
        _productRep = productRep;   
    }
    public async Task<Unit> Handle(ChangeDatetimeProductsQueryReq request, CancellationToken cancellationToken)
    {
        var list=await _productRep.GetAll();
        foreach (var item in list.OrderByDescending(u=>u.CreateDate))
        {
            item.CreateDate = DateTime.Now;
            await _productRep.UpdateAsync(item);
        }
        return Unit.Value;
    }
}