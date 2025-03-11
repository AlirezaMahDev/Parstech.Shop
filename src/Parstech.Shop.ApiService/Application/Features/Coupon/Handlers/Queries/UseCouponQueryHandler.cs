using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Order;
using Shop.Application.DTOs.OrderDetail;
using Shop.Application.Enum;
using Shop.Application.Features.Coupon.Requests.Queries;
using Shop.Application.Features.Order.Requests.Queries;
using Shop.Application.Features.OrderDetail.Requests.Queries;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Coupon.Handlers.Queries
{
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
			OrderResponse Response = new OrderResponse();
			var Order =await _orderRep.GetAsync(request.orderId);
            var orderDetails = await _orderDetailRep.GetOrderDetailsByOrderId(Order.OrderId);
            var Coupon =await _coupoonRep.GetByCouponCode(request.coupon);
			if (Coupon == null)
			{
				Response.Status = false;
				Response.Message = "کد تخفیف وارد شده نامعتبر است";
				Response.Discount = 0;
				return Response;
			}
			var LimitCheck = _coupoonRep.CheckLimitUse(Coupon);
			if (!LimitCheck.Status)
			{
				return LimitCheck;
			}

			var ExpireCheck = _coupoonRep.CheckExpireDate(Coupon);
			if (!ExpireCheck.Status)
			{
				return ExpireCheck;
			}
			var UserList = new List<CouponPcu>();
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

            OrderCoupon oc=new OrderCoupon()
			{
				OrderId=Order.OrderId,
				CouponId=Coupon.Id,
			};
			await _orderCouponRep.AddAsync(oc);



			if(Coupon.CouponTypeId==1|| Coupon.CouponTypeId==2)
			{
                var orderDto = _mapper.Map<OrderDto>(Order);
                Response = await _mediator.Send(new RefreshOrderQueryReq(Order.OrderId));

                
			}
			else
			{
                foreach (var item in orderDetails)
                {
                    var detailDto = _mapper.Map<OrderDetailDto>(item);
                    Response = await _mediator.Send(new RefreshOrderDetailQueryReq(detailDto));
                }
            }
			return Response;
		}
	}
}
