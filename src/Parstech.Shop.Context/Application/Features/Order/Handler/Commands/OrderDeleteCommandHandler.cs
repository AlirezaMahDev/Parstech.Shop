using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Features.Order.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.Order.Handler.Commands;

public class OrderDeleteCommandHandler : IRequestHandler<OrderDeleteCommandReq, Unit>
{
    private readonly IOrderRepository _orderRep;
    private readonly IOrderPayRepository _orderPayRep;
    private readonly IOrderShippingRepository _orderShippingRep;
    private readonly IOrderCouponRepository _orderCouponRep;
    private readonly IOrderStatusRepository _orderStatusRep;
    public OrderDeleteCommandHandler(IOrderRepository orderRep,
        IOrderPayRepository orderPayRep,
        IOrderShippingRepository orderShippingRep,
        IOrderCouponRepository orderCouponRep,
        IOrderStatusRepository orderStatusRep)
    {
        _orderRep = orderRep;
        _orderPayRep = orderPayRep;
        _orderShippingRep = orderShippingRep;
        _orderCouponRep = orderCouponRep;
        _orderStatusRep = orderStatusRep;

    }
    public async Task<Unit> Handle(OrderDeleteCommandReq request, CancellationToken cancellationToken)
    {
        var order = await _orderRep.GetAsync(request.OrderId);
        var shippig = await _orderShippingRep.GetOrderShippingByOrderId(request.OrderId);
        var pays = await _orderPayRep.GetListByOrderId(request.OrderId);
        var statuses = await _orderStatusRep.GetByOrderId(request.OrderId);
        var coupon = await _orderCouponRep.GetByOrderId(request.OrderId);

        if (shippig.Id != 0) { await _orderShippingRep.DeleteAsync(shippig); }
        foreach (var pay in pays)
        {
            if (pay.Id != 0) { await _orderPayRep.DeleteAsync(pay); }
        }
           
        if (coupon != null) { await _orderCouponRep.DeleteAsync(coupon); }
        if (statuses.Count() > 0)
        {
            foreach (var status in statuses)
            {
                await _orderStatusRep.DeleteAsync(status);
            }
                
        }

        await _orderRep.DeleteAsync(order);
        return Unit.Value;
    }
}