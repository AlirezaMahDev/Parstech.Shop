using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Dapper.Helper;
using Shop.Application.DTOs.Response;
using Shop.Application.DTOs.WalletTransaction;
using Shop.Application.Enum;
using Shop.Application.Features.Order.Requests.Queries;
using Shop.Application.Features.OrderPay.Request.Queries;
using Shop.Application.Features.OrderStatus.Requests.Queries;
using Shop.Application.Features.WalletTransaction.Requests.Commands;
using Shop.Application.Features.WalletTransaction.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Order.Handler.Queries
{
    
    public class CompleteOrderForCreditProductQueryHandler : IRequestHandler<CompleteOrderForCreditProductQueryReq, ResponseDto>
    {

        private readonly IMediator _mediator;
        private readonly IOrderRepository _orderRep;
        private readonly IPayTypeRepository _payRep;
        private readonly IOrderPayRepository _orderPayRep;
        private readonly IOrderShippingRepository _orderShippingRep;
        private readonly IUserRepository _userRep;
        private readonly IWalletRepository _walletRep;
        private readonly IWalletTransactionRepository _walletTransactionRep;
        private readonly IOrderCouponRepository _orderCouponRep;
        private readonly IUserBillingRepository _userBillingRep;
        private readonly ISecoundPayAfterDargahRepository _secoundPayAfterDargahRep;
        private readonly string _connectionString;
        public CompleteOrderForCreditProductQueryHandler(IMediator mediator,
            IOrderRepository orderRep,
            IOrderPayRepository orderPayRep,
            IOrderShippingRepository orderShippingRep,
            IPayTypeRepository payRep,
            IUserRepository userRep,
            IWalletRepository walletRep,
            IWalletTransactionRepository walletTransactionRep,
            IOrderCouponRepository orderCouponRep,
            IUserBillingRepository userBillingRep,
            ISecoundPayAfterDargahRepository secoundPayAfterDargahRep, IConfiguration configuration)
        {
            _mediator = mediator;
            _orderRep = orderRep;
            _orderPayRep = orderPayRep;
            _orderShippingRep = orderShippingRep;
            _userRep = userRep;
            _walletRep = walletRep;
            _walletTransactionRep = walletTransactionRep;
            _orderCouponRep = orderCouponRep;
            _userBillingRep = userBillingRep;
            _payRep = payRep;
            _secoundPayAfterDargahRep = secoundPayAfterDargahRep;
            _connectionString = configuration.GetConnectionString("DatabaseConnection");
        }

        public async Task<ResponseDto> Handle(CompleteOrderForCreditProductQueryReq request, CancellationToken cancellationToken)
        {
            ResponseDto Response = new ResponseDto();
            if (request.orderId == 0)
            {
                Response.IsSuccessed = false;
                Response.Message = "سفارش معتبر نمی باشد";
                return Response;
            }
            if (request.orderShippingId == 0)
            {
                Response.IsSuccessed = false;
                Response.Message = "ابتدا آدرسی برای سفارش خود ثبت و مجددا امتحان کنید";
                return Response;
            }
            if (request.payTypeId == 0)
            {
                Response.IsSuccessed = false;
                Response.Message = "روش پرداخت سفارش را انتخاب نمایید";
                return Response;
            }

            var order = await _orderRep.GetAsync(request.orderId);
            var orderShipping = await _orderShippingRep.GetAsync(request.orderShippingId);
            var PayType = await _payRep.GetAsync(request.payTypeId);
            var orderCuopon = await _orderCouponRep.GetByOrderId(order.OrderId);
            var wallet = await _walletRep.GetWalletByUserId(order.UserId);
            var user = await _userRep.GetAsync(order.UserId);
            var userBilling = await _userBillingRep.GetUserBillingByUserId(order.UserId);
            #region OrderPay
            var res = await _mediator.Send(new ChoisePayTypeForCreateOrderPayForCreditProductQueryReq(PayType.Id, wallet, order,request.credit));
            #endregion

            //اگر مشکل داشت
            if (!res.IsSuccessed)
            {
                Response.IsSuccessed = false;
                Response.Message = res.Message;
                return Response;
            }

            //اگر نرمال بود و فقط یک حالت پرداخت داشت
            else if (res.orderPayResult.Count == 1)
            {
                foreach (var item in res.orderPayResult)
                {
                    if (item.Price > 0)
                    {
                        WalletTransactionDto transaction7 = new WalletTransactionDto()
                        {
                            WalletId = wallet.WalletId,
                            CreateDate = DateTime.Now,
                            Description = order.OrderCode,
                            Type = "Amount",
                            TypeId = 3,
                            Price = Convert.ToInt32(item.Price),
                        };


                        var createdTransaction7 = await _mediator.Send(new CreateWalletTransactionCommandReq(transaction7, false));
                        Response.IsSuccessed = true;
                        Response.Message = "در حال انتقال به درگاه پرداخت";



                        Response.Object = await _mediator.Send(new DargaForCreditProductQueryReq(order.OrderCode, createdTransaction7.walletTransaction.Id, item.Price,request.credit));

                        Response.IsSuccessed = true;
                        //ثیت وضعیت
                        await _mediator.Send(new CreateOrderStatusByStatusIdQueryReq(OrderStatusType.OrderAwaitingPayment.ToString(), order.OrderId, order.UserId));

                    }
                    else
                    {
                        Response = await _mediator.Send(new CallBackCompleteOrderQueryReq(order.OrderId, 0, null));
                        await _mediator.Send(new CreateAghsatForCreditProductQueryReq(order, request.credit));
                        //ثیت وضعیت
                        await _mediator.Send(new CreateOrderStatusByStatusIdQueryReq(OrderStatusType.OrderDoing.ToString(), order.OrderId, order.UserId));

                    }

                }
            }
           


            order.CreateDate = DateTime.Now;
            order.CreditId = request.credit.Id;
            await _orderRep.UpdateAsync(order);
            return Response;
        }
    }
}
