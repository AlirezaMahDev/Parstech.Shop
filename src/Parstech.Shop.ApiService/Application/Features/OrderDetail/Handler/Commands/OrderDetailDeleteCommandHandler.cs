using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Order.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.OrderDetail.Requests.Commands;

namespace Parstech.Shop.ApiService.Application.Features.OrderDetail.Handler.Commands;

public class OrderDetailDeleteCommandHandler : IRequestHandler<OrderDetailDeleteCommandReq, Unit>
{
    private readonly IOrderRepository _orderRep;
    private readonly IMediator _mediator;
    private readonly IOrderDetailRepository _orderDetailRep;

    public OrderDetailDeleteCommandHandler(IOrderRepository orderRep,
        IOrderDetailRepository orderDetailRep,
        IMediator mediator)
    {
        _orderRep = orderRep;
        _orderDetailRep = orderDetailRep;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(OrderDetailDeleteCommandReq request, CancellationToken cancellationToken)
    {
        Shared.Models.OrderDetail? item = await _orderDetailRep.GetAsync(request.Id);
        int orderId = item.OrderId;
        await _orderDetailRep.DeleteAsync(item);
        if (await _orderRep.OrderExistAnyDetails(orderId))
        {
        }
        else
        {
            await _mediator.Send(new OrderDeleteCommandReq(orderId));
        }

        return Unit.Value;
    }
}