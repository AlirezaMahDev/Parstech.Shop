using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.OrderPay;
using Shop.Application.DTOs.ProductRepresentation;
using Shop.Application.DTOs.Response;
using Shop.Application.DTOs.WalletTransaction;
using Shop.Application.Enum;
using Shop.Application.Features.Order.Requests.Commands;
using Shop.Application.Features.Order.Requests.Queries;
using Shop.Application.Features.OrderStatus.Requests.Queries;
using Shop.Application.Features.ProductRepresentation.Handlers.Commands;
using Shop.Application.Features.ProductRepresentation.Requests.Commands;
using Shop.Application.Features.ProductRepresentation.Requests.Queries;
using Shop.Application.Generator;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Order.Handler.Queries
{
    public class CallBackCompleteOrderQueryHandler : IRequestHandler<CallBackCompleteOrderQueryReq, ResponseDto>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRep;
        private readonly IOrderPayRepository _orderPayRep;
        private readonly IOrderDetailRepository _orderDetailRep;
        private readonly IOrderCouponRepository _orderCouponRep;
        private readonly ICouponRepository _couponRep;
        private readonly IWalletRepository _walletRep;
        private readonly IWalletTransactionRepository _walletTransactionRep;
        private readonly IProductRepository _productRep;
        private readonly IProductRepresesntationRepository _productReperesntationRep;
        private readonly IProductStockPriceRepository _productStockRep;

        public CallBackCompleteOrderQueryHandler(IMediator mediator , 
         IMapper mapper,
         IOrderRepository orderRep,
         IOrderPayRepository orderPayRep,
         IOrderDetailRepository orderDetailRep,
         IOrderCouponRepository orderCouponRep,
         ICouponRepository couponRep,
         IWalletRepository walletRep,
         IWalletTransactionRepository walletTransactionRep,
         IProductRepository productRep,
         IProductStockPriceRepository productStockRep,
         IProductRepresesntationRepository productReperesntationRep)
        {
            _mediator= mediator;
            _mapper=mapper;
            _orderRep=orderRep;
            _orderPayRep=orderPayRep;
            _orderDetailRep=orderDetailRep;
            _orderCouponRep=orderCouponRep;
            _couponRep=couponRep;
            _walletRep=walletRep;
            _walletTransactionRep=walletTransactionRep;
            _productRep=productRep;
            _productReperesntationRep=productReperesntationRep;
            _productStockRep=productStockRep;
        }



        public async Task<ResponseDto> Handle(CallBackCompleteOrderQueryReq request, CancellationToken cancellationToken)
        {
            ResponseDto Response = new ResponseDto();
            var order =await _orderRep.GetAsync(request.orderId);
            
            var orderDetails=await _orderDetailRep.GetOrderDetailsByOrderId(order.OrderId);
            
            ProductRepresentationDto ProductRep=new ProductRepresentationDto()
            {
                UserId=order.UserId,
                TypeId=2,
            };
            var Coin = 0;

            //کم کردن موجودی
            foreach( var orderDetail in orderDetails )
            {
                
                var productStock = await _productStockRep.GetAsync(orderDetail.ProductStockPriceId);
                var product = await _productRep.GetAsync(productStock.ProductId);

                if (product.TypeId == 4)
                {
                    await _mediator.Send(new CreateProductRepresentationForChildsOfBundleQueryReq(order.UserId, product.Id, productStock.Id, orderDetail.Count)); ;
                }
                else
                {
                    Coin += product.Score;
                    ProductRep.UniqeCode = CodeGenerator.GenerateUniqCode();
                    ProductRep.ProductStockPriceId = productStock.Id;
                    ProductRep.RepresntationId = productStock.RepId;
                    ProductRep.Quantity = orderDetail.Count;
                    await _mediator.Send(new ProductRepresesntationCreateCommandReq(ProductRep));
                }
                #region Refresh Best Stock Id
                if (product.TypeId == 3)
                {
                    await _productRep.RefreshBestStockProduct(product.ParentId.Value);
                }
                else
                {
                    await _productRep.RefreshBestStockProduct(product.Id);
                }
            }

            if (request.transactionId != 0)
            {
				var transaction = await _walletTransactionRep.GetAsync(request.transactionId);
				//واریز و برداشت پس از پرداخت اینترنتی
				if (transaction.TypeId == 3)
				{
					transaction.TypeId = 2;
					transaction.TrackingCode = $"کد پیگیری پرداخت : {request.transactionId}";
					await _walletTransactionRep.UpdateAsync(transaction);
					var transactionDto = _mapper.Map<WalletTransactionDto>(transaction);
					await _walletRep.WalletCalculateTransaction(transactionDto);

					Shop.Domain.Models.WalletTransaction Bardasht = new Shop.Domain.Models.WalletTransaction()
					{
						WalletId = transaction.WalletId,
						TypeId = 1,
						Price = transaction.Price,
						Type = transaction.Type,
						TrackingCode = CodeGenerator.GenerateUniqCode(),
						Description = transaction.Description,
						CreateDate = DateTime.Now,
					};
					await _walletTransactionRep.AddAsync(Bardasht);
					var BardashtDto = _mapper.Map<WalletTransactionDto>(Bardasht);
					await _walletRep.WalletCalculateTransaction(BardashtDto);
				}
				//برداشت از حساب
				else
				{
					var transactionDto = _mapper.Map<WalletTransactionDto>(transaction);
					await _walletRep.WalletCalculateTransaction(transactionDto);
				}
				Response.Object2 = transaction.Description;
			}



            var wallet =await _walletRep.GetWalletByUserId(order.UserId);
			//امتیازات
			Shop.Domain.Models.WalletTransaction CoinTransaction = new Shop.Domain.Models.WalletTransaction()
            {
                WalletId = wallet.WalletId,
                TypeId = 2,
                Price = Coin,
                Type = "Coin",
                TrackingCode = CodeGenerator.GenerateUniqCode(),
                Description = $"افزایش امتیاز سفارش {order.OrderCode}",
                CreateDate = DateTime.Now,
            };
            await _walletTransactionRep.AddAsync(CoinTransaction);
            var CoinDto = _mapper.Map<WalletTransactionDto>(CoinTransaction);
            await _walletRep.WalletCalculateTransaction(CoinDto);



            // کم کردن تخفیف
            if (await _orderCouponRep.ExistInOrder(order.OrderId))
            {
                var orderCoupon=await _orderCouponRep.GetByOrderId(order.OrderId);
                var coupon =await _couponRep.GetAsync(orderCoupon.CouponId);
                if(coupon.CouponTypeId == 2)
                {
                    coupon.Amount -= orderCoupon.DiscountPrice;
                }
                coupon.LimitUse--;
                await _couponRep.UpdateAsync(coupon);
            }
            order.IsFinaly = true;
            order.ConfirmPayment = true;
            await _orderRep.UpdateAsync(order);

            ////ثیت وضعیت
            await _mediator.Send(new CreateOrderStatusByStatusIdQueryReq(OrderStatusType.OrderDoing.ToString(),order.OrderId,order.UserId));

           

            #endregion


            Response.IsSuccessed = true;
            Response.Message = "سفارش شما با موفقیت پرداخت شد";
            //Response.Object = $"{transaction.TrackingCode}";
            
            return Response;
        }
    }
}
