using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Coupon.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.Order.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.OrderShipping.Request.Queries;
using Parstech.Shop.ApiService.Domain.Models;

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
        Domain.Models.Order? Order = await _orderRep.GetAsync(request.id);
        if (Order == null)
        {
            return result;
        }

        List<Domain.Models.OrderDetail> orderDetils = await _orderDetailRep.GetOrderDetailsByOrderId(request.id);
        OrderCoupon? orderCoupon = await _orderCouponRep.GetByOrderId(Order.OrderId);
        long OrderSum = 0;
        long Discount = 0;
        long Tax = 0;
        long OrderTotal = 0;

        long orderCouponDiscount = 0;
        foreach (Domain.Models.OrderDetail deteil in orderDetils)
        {
            OrderSum += deteil.DetailSum;
            orderCouponDiscount += deteil.Discount;
            Tax += deteil.Tax;
            OrderTotal += deteil.Total;
        }

        Order.OrderSum = OrderSum;
        Order.Shipping =
            await _mediator.Send(new OrderShippingChangeQueryReq("Refresh", 0, Order.OrderId, Order.OrderSum));
        Order.Tax = Tax;

        if (await _orderCouponRep.OrderHaveCoupon(Order.OrderId))
        {
            OrderDto? orderDto = _mapper.Map<OrderDto>(Order);

            Domain.Models.Coupon? coupon = await _couponRep.GetAsync(orderCoupon.CouponId);
            if (coupon.CouponTypeId == 1 || coupon.CouponTypeId == 2)
            {
                void DisountResult =
                    await _mediator.Send(new CheckAndUseCouponQueryReq("Order", orderDto, null, coupon.Code));
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

        Order.Total = OrderTotal - Discount + Order.Shipping;
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