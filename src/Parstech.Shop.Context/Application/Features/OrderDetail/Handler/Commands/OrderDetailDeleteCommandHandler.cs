using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Features.Order.Requests.Commands;
using Parstech.Shop.Context.Application.Features.OrderDetail.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.OrderDetail.Handler.Commands;

public class OrderDetailDeleteCommandHandler : IRequestHandler<OrderDetailDeleteCommandReq, Unit>
{
    private readonly IOrderRepository _orderRep;
    private readonly IMediator _mediator;
    private readonly IOrderDetailRepository _orderDetailRep;
    public OrderDetailDeleteCommandHandler(IOrderRepository orderRep, IOrderDetailRepository orderDetailRep,IMediator mediator)
    {
        _orderRep= orderRep;
        _orderDetailRep= orderDetailRep;
        _mediator= mediator;
    }
    public async Task<Unit> Handle(OrderDetailDeleteCommandReq request, CancellationToken cancellationToken)
    {
        var item =await _orderDetailRep.GetAsync(request.Id);
        var orderId = item.OrderId;
        await _orderDetailRep.DeleteAsync(item);
        if(await _orderRep.OrderExistAnyDetails(orderId))
        {

        }
        else
        {
            await _mediator.Send(new OrderDeleteCommandReq(orderId));
        }
        return Unit.Value;
    }
}