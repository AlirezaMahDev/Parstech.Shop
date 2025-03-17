using MediatR;

using Parstech.Shop.ApiService.Application.Features.Product.Requests.Commands;

namespace Parstech.Shop.ApiService.Application.Features.Product.Handlers.Commands;

public class ProductDeleteCommandHandler : IRequestHandler<ProductDeleteCommandReq, Unit>
{
    public Task<Unit> Handle(ProductDeleteCommandReq request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}