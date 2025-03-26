using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.OrderStatus;
using Parstech.Shop.Context.Application.Features.OrderStatus.Requests.Commands;
using Parstech.Shop.Context.Application.Features.OrderStatus.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.OrderStatus.Handler.Queries;

public class CreateOrderStatusByStatusIdQueryHandler : IRequestHandler<CreateOrderStatusByStatusIdQueryReq, Unit>
{

    private readonly IStatusRepository _statusRep;
    private readonly IUserRepository _userRep;

    private readonly IMediator _mediator;
    public CreateOrderStatusByStatusIdQueryHandler(
        IUserRepository userRep,
        IStatusRepository statusRep,
        IMediator mediator)
    {

        _statusRep = statusRep;
        _userRep= userRep;
        _mediator = mediator;
    }
    public async Task<Unit> Handle(CreateOrderStatusByStatusIdQueryReq request, CancellationToken cancellationToken)
    {
        var status = await _statusRep.GetStatusByLatinName(request.Latinstatus);
        var user = await _userRep.GetAsync(request.userId);
        OrderStatusDto item = new();
        item.StatusId = status.Id;
        item.OrderId = request.orderId;
        item.CreateDate = DateTime.Now;
        item.CreateBy = user.UserName;

        await _mediator.Send(new OrderStatusCreatCommandReq(item));
        return Unit.Value;
    }
}