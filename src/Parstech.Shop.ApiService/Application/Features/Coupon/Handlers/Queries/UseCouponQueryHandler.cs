using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Enum;
using Parstech.Shop.ApiService.Application.Features.Coupon.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.Order.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.OrderDetail.Requests.Queries;
using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Features.Coupon.Handlers.Queries;

public class UseCouponQueryHandler : IRequestHandler<UseCouponQueryReq, OrderResponse>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRep;
    private readonly IOrderDetailRepository _orderDetailRep;
    private readonly ICouponRepository _coupoonRep;
    private readonly IOrderCouponRepository _orderCouponRep;
    private readonly ICouponPcuRepository _coupoonPcuRep;

    public UseCouponQueryHandler(IOrderRepository orderRep,
        IMediator mediator,
        IMapper mapper,
        IOrderDetailRepository orderDetailRep,
        ICouponRepository coupoonRep,
        ICouponPcuRepository coupoonPcuRep,
        IOrderCouponRepository orderCouponRep)
    {
        _orderRep = orderRep;
        _mediator = mediator;
        _mapper = mapper;
        _coupoonRep = coupoonRep;
        _coupoonPcuRep = coupoonPcuRep;
        _orderDetailRep = orderDetailRep;
        _orderCouponRep = orderCouponRep;
    }

    public async Task<OrderResponse> Handle(UseCouponQueryReq request, CancellationToken cancellationToken)
    {
        OrderResponse Response = new();
        Shared.Models.Order? Order = await _orderRep.GetAsync(request.orderId);
        List<Shared.Models.OrderDetail> orderDetails = await _orderDetailRep.GetOrderDetailsByOrderId(Order.OrderId);
        Shared.Models.Coupon? Coupon = await _coupoonRep.GetByCouponCode(request.coupon);
        if (Coupon == null)
        {
            Response.Status = false;
            Response.Message = "کد تخفیف وارد شده نامعتبر است";
            Response.Discount = 0;
            return Response;
        }

        OrderResponse LimitCheck = _coupoonRep.CheckLimitUse(Coupon);
        if (!LimitCheck.Status)
        {
            return LimitCheck;
        }

        OrderResponse ExpireCheck = _coupoonRep.CheckExpireDate(Coupon);
        if (!ExpireCheck.Status)
        {
            return ExpireCheck;
        }

        List<CouponPcu> UserList = new();
        if (Coupon.Users == CouponEnum.All.ToString())
        {
            UserList = await _coupoonPcuRep.GetPCUOfCoupon(CouponEnum.Users.ToString(), false, Coupon);
        }
        else
        {
            UserList = await _coupoonPcuRep.GetPCUOfCoupon(CouponEnum.Users.ToString(), true, Coupon);
        }

        if (UserList.Any(u => u.FkId == Order.UserId))
        {
            Response.Status = false;
            Response.Message = "شما اجازه اسفاده از این کد تخفیف را ندارید";
            return Response;
        }

        if (await _orderCouponRep.OrderHaveCoupon(Order.OrderId))
        {
            Response.Status = false;
            Response.Message = "برای هر سفارش تنها یکبار میتوان از کد تخفیف استفاده کرد";
            return Response;
        }

        OrderCoupon oc = new() { OrderId = Order.OrderId, CouponId = Coupon.Id };
        await _orderCouponRep.AddAsync(oc);


        if (Coupon.CouponTypeId == 1 || Coupon.CouponTypeId == 2)
        {
            OrderDto? orderDto = _mapper.Map<OrderDto>(Order);
            Response = await _mediator.Send(new RefreshOrderQueryReq(Order.OrderId));
        }
        else
        {
            foreach (Shared.Models.OrderDetail item in orderDetails)
            {
                OrderDetailDto? detailDto = _mapper.Map<OrderDetailDto>(item);
                Response = await _mediator.Send(new RefreshOrderDetailQueryReq(detailDto));
            }
        }

        return Response;
    }
}