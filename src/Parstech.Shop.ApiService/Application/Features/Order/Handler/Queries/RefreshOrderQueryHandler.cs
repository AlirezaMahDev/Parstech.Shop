using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Coupon.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.Order.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.OrderShipping.Request.Queries;
using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Features.Order.Handler.Queries;

public class RefreshOrderQueryHandler : IRequestHandler<RefreshOrderQueryReq, OrderResponse>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRep;
    private readonly IOrderDetailRepository _orderDetailRep;
    private readonly IOrderCouponRepository _orderCouponRep;
    private readonly ICouponRepository _couponRep;
    private readonly IProductStockPriceRepository _productStockRep;

    public RefreshOrderQueryHandler(IMediator mediator,
        IMapper mapper,
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
        Shared.Models.Order? order = await _orderRep.GetAsync(request.id);
        if (order == null)
        {
            return result;
        }

        List<Shared.Models.OrderDetail> orderDetils = await _orderDetailRep.GetOrderDetailsByOrderId(request.id);
        OrderCoupon? orderCoupon = await _orderCouponRep.GetByOrderId(order.OrderId);
        long orderSum = 0;
        long discount = 0;
        long tax = 0;
        long orderTotal = 0;

        long orderCouponDiscount = 0;
        foreach (Shared.Models.OrderDetail deteil in orderDetils)
        {
            orderSum += deteil.DetailSum;
            orderCouponDiscount += deteil.Discount;
            tax += deteil.Tax;
            orderTotal += deteil.Total;
        }

        order.OrderSum = orderSum;
        order.Shipping =
            await _mediator.Send(new OrderShippingChangeQueryReq("Refresh", 0, order.OrderId, order.OrderSum));
        order.Tax = tax;

        if (await _orderCouponRep.OrderHaveCoupon(order.OrderId))
        {
            OrderDto? orderDto = _mapper.Map<OrderDto>(order);

            Shared.Models.Coupon? coupon = await _couponRep.GetAsync(orderCoupon.CouponId);
            if (coupon.CouponTypeId == 1 || coupon.CouponTypeId == 2)
            {
                var disountResult =
                    await _mediator.Send(new CheckAndUseCouponQueryReq("Order", orderDto, null, coupon.Code));
                discount += disountResult.Discount;
                orderCouponDiscount += discount;
                result = disountResult;
            }
            else
            {
                result.Status = true;
                result.Message = "عملیات با موفقیت انجام شد";
                discount += 0;
            }
        }
        else
        {
            result.Status = true;
            result.Message = "عملیات با موفقیت انجام شد";
            discount = 0;
        }

        order.Discount = discount;
        //var total = OrderDetailCalculator.GetTotal(Order.OrderSum, Order.Tax, orderCouponDiscount);

        order.Total = orderTotal - discount + order.Shipping;
        await _orderRep.UpdateAsync(order);

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