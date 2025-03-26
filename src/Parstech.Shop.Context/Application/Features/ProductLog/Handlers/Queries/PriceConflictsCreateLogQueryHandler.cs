using MediatR;
using Parstech.Shop.Context.Application.Features.ProductLog.Requests.Commands;
using Parstech.Shop.Context.Application.Features.ProductLog.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.ProductLog.Handlers.Queries;

public class PriceConflictsCreateLogQueryHandler : IRequestHandler<PriceConflictsCreateLogQueryReq, Unit>
{
    private readonly IMediator _mediator;

    public PriceConflictsCreateLogQueryHandler(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<Unit> Handle(PriceConflictsCreateLogQueryReq request, CancellationToken cancellationToken)
    {

        if (request.crrentProduct.Price != request.EditProduct.Price)
        {
            await _mediator.Send(new ProductLogCreateCommandReq(1,
                request.userName,
                request.crrentProduct.Price.ToString(),
                request.EditProduct.Price.ToString(),
                request.EditProduct.Id));
        }
        if (request.crrentProduct.SalePrice != request.EditProduct.SalePrice)
        {
            await _mediator.Send(new ProductLogCreateCommandReq(2,
                request.userName,
                request.crrentProduct.SalePrice.ToString(),
                request.EditProduct.SalePrice.ToString(),
                request.EditProduct.Id));
        }
        if (request.crrentProduct.DiscountPrice != request.EditProduct.DiscountPrice)
        {
            await _mediator.Send(new ProductLogCreateCommandReq(3,
                request.userName,
                request.crrentProduct.DiscountPrice.ToString(),
                request.EditProduct.DiscountPrice.ToString(),
                request.EditProduct.Id));
        }
        if (request.crrentProduct.BasePrice != request.EditProduct.BasePrice)
        {
            await _mediator.Send(new ProductLogCreateCommandReq(4,
                request.userName,
                request.crrentProduct.BasePrice.ToString(),
                request.EditProduct.BasePrice.ToString(),
                request.EditProduct.Id));
        }

        return Unit.Value;
    }
}