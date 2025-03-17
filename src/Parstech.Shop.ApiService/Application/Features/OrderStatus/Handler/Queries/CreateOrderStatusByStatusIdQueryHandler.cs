using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.OrderStatus.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.OrderStatus.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.OrderStatus.Handler.Queries;

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
        _userRep = userRep;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(CreateOrderStatusByStatusIdQueryReq request, CancellationToken cancellationToken)
    {
        Shared.Models.Status? status = await _statusRep.GetStatusByLatinName(request.Latinstatus);
        Shared.Models.User? user = await _userRep.GetAsync(request.userId);
        OrderStatusDto item = new();
        item.StatusId = status.Id;
        item.OrderId = request.orderId;
        item.CreateDate = DateTime.Now;
        item.CreateBy = user.UserName;

        await _mediator.Send(new OrderStatusCreatCommandReq(item));
        return Unit.Value;
    }
}