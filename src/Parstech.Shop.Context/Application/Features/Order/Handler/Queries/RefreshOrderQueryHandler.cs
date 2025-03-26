using AutoMapper;

using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Order;
using Parstech.Shop.Context.Application.Features.Coupon.Requests.Queries;
using Parstech.Shop.Context.Application.Features.Order.Requests.Queries;
using Parstech.Shop.Context.Application.Features.OrderShipping.Request.Queries;

namespace Parstech.Shop.Context.Application.Features.Order.Handler.Queries;

public class RefreshOrderQueryHandler : IRequestHandler<RefreshOrderQueryReq, OrderResponse>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRep;
    private readonly IOrderDetailRepository _orderDetailRep;
    private readonly IOrderCouponRepository _orderCouponRep;
    private readonly ICouponRepository _couponRep;
    private readonly IProductStockPriceRepository _productStockRep;

    public RefreshOrderQueryHandler(IMediator mediator, IMapper mapper,
        IOrderDetailRepository orderDetailRep,
        IOrderRepository orderRep,
        IOrderCouponRepository orderCouponRep,
        ICouponRepository couponRep,
        IProductStockPriceRepository productStockRep)
    {
        _mediator = mediator;
        _orderRep = orderRep;
        _orderDetailRep = orderDetailRep;
        _orderCouponRep = orderCouponRep;
        _couponRep = couponRep;
        _mapper = mapper;
        _productStockRep = productStockRep;
    }
    public async Task<OrderResponse> Handle(RefreshOrderQueryReq request, CancellationToken cancellationToken)
    {
        OrderResponse result = new();
        var Order = await _orderRep.GetAsync(request.id);
        if (Order == null)
        {
            return result;
        }
        var orderDetils = await _orderDetailRep.GetOrderDetailsByOrderId(request.id);
        var orderCoupon = await _orderCouponRep.GetByOrderId(Order.OrderId);
        long OrderSum = 0;
        long Discount = 0;
        long Tax = 0;
        long OrderTotal = 0;

        long orderCouponDiscount = 0;
        foreach (var deteil in orderDetils)
        {
            OrderSum += deteil.DetailSum;
            orderCouponDiscount += deteil.Discount;
            Tax += deteil.Tax;
            OrderTotal += deteil.Total;
        }
        Order.OrderSum = OrderSum;
        Order.Shipping = await _mediator.Send(new OrderShippingChangeQueryReq("Refresh", 0, Order.OrderId, Order.OrderSum));
        Order.Tax = Tax;

        if (await _orderCouponRep.OrderHaveCoupon(Order.OrderId))
        {
            var orderDto = _mapper.Map<OrderDto>(Order);

            var coupon = await _couponRep.GetAsync(orderCoupon.CouponId);
            if (coupon.CouponTypeId == 1 || coupon.CouponTypeId == 2)
            {
                var DisountResult = await _mediator.Send(new CheckAndUseCouponQueryReq("Order", orderDto, null, coupon.Code));
                Discount += DisountResult.Discount;
                orderCouponDiscount += Discount;
                result = DisountResult;
            }
            else
            {
                result.Status = true;
                result.Message = "عملیات با موفقیت انجام شد";
                Discount += 0;
            }

        }
        else
        {
            result.Status = true;
            result.Message = "عملیات با موفقیت انجام شد";
            Discount = 0;
        }
        Order.Discount = Discount;
        //var total = OrderDetailCalculator.GetTotal(Order.OrderSum, Order.Tax, orderCouponDiscount);

        Order.Total = (OrderTotal - Discount) + Order.Shipping;
        await _orderRep.UpdateAsync(Order);

        try
        {
            if (orderCoupon != null)
            {
                orderCoupon.DiscountPrice = orderCouponDiscount;
                await _orderCouponRep.UpdateAsync(orderCoupon);
            }
        }
        catch (Exception ex)
        {

        }


        return result;

    }
}