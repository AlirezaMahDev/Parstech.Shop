using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Order.Requests.Commands;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Features.Order.Handler.Commands;

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
        Shared.Models.Order? order = await _orderRep.GetAsync(request.OrderId);
        Shared.Models.OrderShipping shippig = await _orderShippingRep.GetOrderShippingByOrderId(request.OrderId);
        List<Shared.Models.OrderPay> pays = await _orderPayRep.GetListByOrderId(request.OrderId);
        List<Shared.Models.OrderStatus> statuses = await _orderStatusRep.GetByOrderId(request.OrderId);
        OrderCoupon? coupon = await _orderCouponRep.GetByOrderId(request.OrderId);

        if (shippig.Id != 0) { await _orderShippingRep.DeleteAsync(shippig); }

        foreach (Shared.Models.OrderPay? pay in pays)
        {
            if (pay.Id != 0) { await _orderPayRep.DeleteAsync(pay); }
        }

        if (coupon != null) { await _orderCouponRep.DeleteAsync(coupon); }

        if (statuses.Count() > 0)
        {
            foreach (Shared.Models.OrderStatus? status in statuses)
            {
                await _orderStatusRep.DeleteAsync(status);
            }
        }

        await _orderRep.DeleteAsync(order);
        return Unit.Value;
    }
}