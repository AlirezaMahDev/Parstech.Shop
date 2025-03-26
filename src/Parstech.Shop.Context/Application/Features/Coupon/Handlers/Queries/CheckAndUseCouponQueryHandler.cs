using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Order;
using Parstech.Shop.Context.Application.Enum;
using Parstech.Shop.Context.Application.Features.Coupon.Requests.Queries;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Features.Coupon.Handlers.Queries;

public class CheckAndUseCouponQueryHandler : IRequestHandler<CheckAndUseCouponQueryReq, OrderResponse>
{
    private readonly IOrderRepository _orderRep;
    private readonly IOrderDetailRepository _orderDetailRep;
    private readonly ICouponRepository _coupoonRep;
    private readonly IOrderCouponRepository _orderCoupoonRep;
    private readonly ICouponPcuRepository _coupoonPcuRep;
    public CheckAndUseCouponQueryHandler(IOrderRepository orderRep,
        IOrderDetailRepository orderDetailRep,
        ICouponRepository coupoonRep,
        IOrderCouponRepository orderCoupoonRep,
        ICouponPcuRepository coupoonPcuRep)
    {
        _orderRep = orderRep;
        _coupoonRep = coupoonRep;
        _coupoonPcuRep = coupoonPcuRep;
        _orderCoupoonRep = orderCoupoonRep;
    }
    public async Task<OrderResponse> Handle(CheckAndUseCouponQueryReq request, CancellationToken cancellationToken)
    {
        OrderResponse Response = new();
        var Coupon = await _coupoonRep.GetByCouponCode(request.CouponCode);
			
			


        long Discount = 0;
        if (request.orderORdetail == "Order")
        {
            var Order = request.orderDto;
				
            var MinPriceCheck = _coupoonRep.CheckMinPrice(Order.OrderSum, Coupon);
            if (!MinPriceCheck.Status)
            {
                await _orderCoupoonRep.CheckAndDelete(Order.OrderId);
                return MinPriceCheck;
            }
            var MaxPriceCheck = _coupoonRep.CheckMaxPrice(Order.OrderSum, Coupon);
            if (!MaxPriceCheck.Status)
            {
                await _orderCoupoonRep.CheckAndDelete(Order.OrderId);
                return MaxPriceCheck;
            }

            //اعمال 
            Discount = _coupoonRep.CalculateDiscount(Coupon, Order.OrderSum);
            Order.Discount = Discount;
            Response.Discount = Discount;
            var orderCoupon =await _orderCoupoonRep.GetByOrderId(Order.OrderId);
            orderCoupon.DiscountPrice = Discount;
            await _orderCoupoonRep.UpdateAsync(orderCoupon);

        }
        else
        {
				
            var OrderDetail = request.orderDetailDto;
            var Order = await _orderRep.GetAsync(OrderDetail.OrderId);
				
            var ProductList = new List<CouponPcu>();

            if (Coupon.Products == CouponEnum.All.ToString())
            {
                ProductList = await _coupoonPcuRep.GetPCUOfCoupon(CouponEnum.Products.ToString(), false, Coupon);

                if (ProductList.Any(u => u.FkId == OrderDetail.Id))
                {
                    await _orderCoupoonRep.CheckAndDelete(Order.OrderId);
                    Response.Status = false;
                    Response.Message = "کد تخفیف وارد شده برای هیچکدام از قلم های سفارش شما معتبر نیست";
                    Response.Discount = Discount;
                    return Response;
                }
            }
            else
            {
                ProductList = await _coupoonPcuRep.GetPCUOfCoupon(CouponEnum.Products.ToString(), true, Coupon);
                List<int> productsList = new();

                if (!ProductList.Any(u => u.FkId == OrderDetail.Id))
                {
                    await _orderCoupoonRep.CheckAndDelete(Order.OrderId);
                    Response.Status = false;
                    Response.Message = "کد تخفیف وارد شده برای هیچکدام از قلم های سفارش شما معتبر نیست";
                    Response.Discount = Discount;
                    return Response;
                }

            }
				
				
            var MinPriceCheck = _coupoonRep.CheckMinPrice(OrderDetail.DetailSum, Coupon);
            var MaxPriceCheck = _coupoonRep.CheckMaxPrice(OrderDetail.DetailSum, Coupon);
            if (MinPriceCheck.Status && MaxPriceCheck.Status)
            {
                Discount = _coupoonRep.CalculateDiscount(Coupon, OrderDetail.DetailSum);
                OrderDetail.Discount = Discount;
                Response.Discount = Discount;
                   
            }
            else
            {
                await _orderCoupoonRep.CheckAndDelete(Order.OrderId);
                Response.Status = false;
                Response.Message = "میزان مبلغ سفارش کمتر و یا بیشتر از حد مجاز میباشد";
                return Response;
            }
        }

			
        Response.Status = true;
        Response.Message = "تخفیف با موفقیت بر روی سبد خرید شما اعمال گردید";
        return Response;

    }
}