using AutoMapper;
using MediatR;
using Shop.Application.Calculator;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.OrderDetail;
using Shop.Application.Enum;
using Shop.Application.Features.Coupon.Requests.Queries;
using Shop.Application.Features.Order.Requests.Queries;
using Shop.Application.Features.OrderDetail.Requests.Commands;
using Shop.Application.Features.OrderDetail.Requests.Queries;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.OrderDetail.Handler.Queries
{
	public class RefreshOrderDetailQueryHandler : IRequestHandler<RefreshOrderDetailQueryReq, OrderResponse>
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;
		private readonly IOrderDetailRepository _orderDetailRep;
		private readonly IUserRepository _userRep;
		private readonly IOrderCouponRepository _orderCouponRep;
		private readonly IOrderRepository _orderRep;
		private readonly ICouponRepository _couponRep;
		private readonly IProductRepository _productRep;
		private readonly IProductStockPriceRepository _productStockRep;

        public RefreshOrderDetailQueryHandler(IMediator mediator, IMapper mapper,
            IOrderDetailRepository orderDetailRep,
            IOrderCouponRepository orderCouponRep,
            ICouponRepository couponRep,
            IProductRepository productRep,
            IProductStockPriceRepository productStockRep, IUserRepository userRep, IOrderRepository orderRep)
        {
            _mediator = mediator;
            _orderDetailRep = orderDetailRep;
            _orderCouponRep = orderCouponRep;
            _couponRep = couponRep;
            _mapper = mapper;
            _productRep = productRep;
            _productStockRep = productStockRep;
            _userRep = userRep;
            _orderRep = orderRep;
        }
        public async Task<OrderResponse> Handle(RefreshOrderDetailQueryReq request, CancellationToken cancellationToken)
		{
			OrderResponse result = new OrderResponse();
			result.Status = true;
			var Request = request.OrderDetailDto;
			var Detail = await _orderDetailRep.GetAsync(Request.Id);

			var stockPrice =await _productStockRep.GetAsync(Detail.ProductStockPriceId);
			var product = await _productRep.GetAsync(stockPrice.ProductId);
			long Price = 0;
            if (request.IsCredit)
            {
                Price = Detail.Price;
            }
            else
            {
                #region Check UserCategury
                var order = await _orderRep.GetAsync(Request.OrderId);
                var user = await _userRep.GetAsync(order.UserId);
                var existUserCategury = await _userRep.ExistUserCategury(user.UserName);
                if (stockPrice.CateguryOfUserId != null)
                {
                    if (!existUserCategury)
                    {
                        if (stockPrice.CateguryOfUserType == CateguryOfUserType.ShowDiscoutProductForUserCategury.ToString())
                        {
                            Price = stockPrice.SalePrice;
                        }
                        else if (stockPrice.CateguryOfUserType == CateguryOfUserType.ShowProductJustForUserCategury.ToString())
                        {
                            Price = 0;
                        }
                    }
                    else
                    {
                        if (stockPrice.DiscountPrice > 0)
                        {
                            Price = stockPrice.DiscountPrice;
                        }
                        else
                        {
                            Price = stockPrice.SalePrice;
                        }
                    }
                }
                else
                {
                    if (stockPrice.DiscountPrice > 0)
                    {
                        Price = stockPrice.DiscountPrice;
                    }
                    else
                    {
                        Price = stockPrice.SalePrice;
                    }
                }
                #endregion
            }




            //if (stockPrice.DiscountPrice > 0)
            //{
            //    Price = stockPrice.DiscountPrice;
            //}
            //else
            //{
            //    Price = stockPrice.SalePrice;
            //}




            if (Request.Count==1000)
			{
				if(Detail.Count>1)
				{
                    Detail.Count--;
                }
				
			}
			else if(Request.Count==1001) 
			{
				
				if (Detail.Count == stockPrice.Quantity)
				{
					result.Status = false;
					result.Message = "موجودی محصول درخواستی در انبار فروشنده کافی نیست ";
					return result;
				}
				else
				{
					if (Detail.Count == stockPrice.MaximumSaleInOrder)
					{
						result.Status = false;
						result.Message = "تعداد محصول انتخابی بیشتر از حد مجاز میباشد ";
						return result;
					}
					else
					{
						Detail.Count++;
					}
				}

			}




            switch (product.TaxId)
            {
                //قیمت گذاری با مالیات
                case 1:
                    var withoutTax = PersentCalculator.BaseCalculatorByPrice(Price);
                    Detail.Price = withoutTax;
                    Detail.DetailSum = OrderDetailCalculator.GetDetailSum(Detail.Price, Detail.Count);
                    //Detail.Tax = PersentCalculator.PersentCalculatorByPrice(Detail.DetailSum, 9);
                    Detail.Tax = (Price * Detail.Count) - (withoutTax * Detail.Count);
                    if (await _orderCouponRep.OrderHaveCoupon(Detail.OrderId))
                    {
                        var dto = _mapper.Map<OrderDetailDto>(Detail);
                        var orderCoupon = await _orderCouponRep.GetByOrderId(Detail.OrderId);
                        var coupon = await _couponRep.GetAsync(orderCoupon.CouponId);
                        if (coupon.CouponTypeId == 3 || coupon.CouponTypeId == 4)
                        {
                            var DisountResult = await _mediator.Send(new CheckAndUseCouponQueryReq("Detail", null, dto, coupon.Code));
                            Detail.Discount = DisountResult.Discount;
                            if (DisountResult.Status == false)
                            {
                                result = DisountResult;
                            }
                            else
                            {
                                result.Status = true;
                                result.Message = "عملیات با موفقیت انجام شد";
                            }

                        }
                        else
                        {
                            if (result.Status != false)
                            {
                                result.Status = true;
                                result.Message = "عملیات با موفقیت انجام شد";
                            }
                            Detail.Discount = 0;
                        }

                    }
                    else
                    {
                        if (result.Status != false)
                        {
                            result.Status = true;
                            result.Message = "عملیات با موفقیت انجام شد";
                        }

                        Detail.Discount = 0;
                    }
                    Detail.Total = OrderDetailCalculator.GetDetailSum(Price, Detail.Count);
                    Detail.Total-= Detail.Discount;
                    var FinalDetail = _mapper.Map<OrderDetailDto>(Detail);

                    await _orderDetailRep.UpdateAsync(Detail);
                    break;

                //محاسبه مالیات پس از قیمت گذاری
                case 2:
                    Detail.DetailSum = OrderDetailCalculator.GetDetailSum(Detail.Price, Detail.Count);
                    Detail.Tax = PersentCalculator.PersentCalculatorByPrice(Detail.DetailSum, 9);
                    if (await _orderCouponRep.OrderHaveCoupon(Detail.OrderId))
                    {
                        var dto = _mapper.Map<OrderDetailDto>(Detail);
                        var orderCoupon = await _orderCouponRep.GetByOrderId(Detail.OrderId);
                        var coupon = await _couponRep.GetAsync(orderCoupon.CouponId);
                        if (coupon.CouponTypeId == 1 || coupon.CouponTypeId == 3)
                        {
                            var DisountResult = await _mediator.Send(new CheckAndUseCouponQueryReq("Detail", null, dto, coupon.Code));
                            Detail.Discount = DisountResult.Discount;
                            if (DisountResult.Status == false)
                            {
                                result = DisountResult;
                            }
                            else
                            {
                                result.Status = true;
                                result.Message = "عملیات با موفقیت انجام شد";
                            }

                        }
                        else
                        {
                            if (result.Status != false)
                            {
                                result.Status = true;
                                result.Message = "عملیات با موفقیت انجام شد";
                            }
                            Detail.Discount = 0;
                        }

                    }
                    else
                    {
                        if (result.Status != false)
                        {
                            result.Status = true;
                            result.Message = "عملیات با موفقیت انجام شد";
                        }

                        Detail.Discount = 0;
                    }
                    Detail.Total = OrderDetailCalculator.GetTotal(Detail.DetailSum, Detail.Tax, Detail.Discount);
                    var FinalDetail2 = _mapper.Map<OrderDetailDto>(Detail);

                    await _orderDetailRep.UpdateAsync(Detail);


                    break;
            }
            await _mediator.Send(new RefreshOrderQueryReq(Detail.OrderId));
            return result;
		}
	}
}
