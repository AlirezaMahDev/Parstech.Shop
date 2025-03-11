using Dapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Dapper.Helper;
using Shop.Application.DTOs.OrderPay;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.Response;
using Shop.Application.DTOs.WalletTransaction;
using Shop.Application.Enum;
using Shop.Application.Features.Api.Requests.Queries;
using Shop.Application.Features.Order.Requests.Queries;
using Shop.Application.Features.OrderPay.Request.Queries;
using Shop.Application.Features.OrderStatus.Requests.Queries;
using Shop.Application.Features.WalletTransaction.Requests.Commands;
using Shop.Application.Features.WalletTransaction.Requests.Queries;
using Shop.Application.Url;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Shop.Application.Features.Order.Handler.Queries
{
    public class CompleteOrderQueryHandler : IRequestHandler<CompleteOrderQueryReq, ResponseDto>
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
        public CompleteOrderQueryHandler(IMediator mediator,
            IOrderRepository orderRep,
            IOrderPayRepository orderPayRep,
            IOrderShippingRepository orderShippingRep,
            IPayTypeRepository payRep,
            IUserRepository userRep,
            IWalletRepository walletRep,
            IWalletTransactionRepository walletTransactionRep,
            IOrderCouponRepository orderCouponRep,
            IUserBillingRepository userBillingRep,
            ISecoundPayAfterDargahRepository secoundPayAfterDargahRep,IConfiguration configuration)
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

        public async Task<ResponseDto> Handle(CompleteOrderQueryReq request, CancellationToken cancellationToken)
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
            var res = await _mediator.Send(new ChoisePayTypeForCreateOrderPayQueryReq(PayType.Id, wallet, order));
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
                    switch (item.PayTypeId)
                    {
                        //پرداخت الکتورنیکی
                        case 1:
                            WalletTransactionDto transaction1 = new WalletTransactionDto()
                            {
                                WalletId = wallet.WalletId,
                                CreateDate = DateTime.Now,
                                Description = order.OrderCode,
                                Type = "Amount",
                                TypeId = 3,
                                Price = Convert.ToInt32(item.Price),
                            };
                            //ثیت وضعیت
                            await _mediator.Send(new CreateOrderStatusByStatusIdQueryReq(OrderStatusType.OrderAwaitingPayment.ToString(), order.OrderId, order.UserId));


                            var createdTransaction1 = await _mediator.Send(new CreateWalletTransactionCommandReq(transaction1, false));
                            Response.IsSuccessed = true;
                            Response.Message = "در حال انتقال به درگاه پرداخت";



                            Response.Object = await _mediator.Send(new DargaQueryReq(order.OrderCode, createdTransaction1.walletTransaction.Id,item.Price));

                            Response.IsSuccessed = true;
                            //ثیت وضعیت
                            await _mediator.Send(new CreateOrderStatusByStatusIdQueryReq(OrderStatusType.OrderAwaitingPayment.ToString(), order.OrderId, order.UserId));


                            return Response;

                        //از کیف پول
                        case 2:

                            if (order.Total > wallet.Amount)
                            {
                                Response.IsSuccessed = false;
                                Response.Message = "موجودی حساب شما جهت تسویه سفارش کافی نیست ";
                                //ثیت وضعیت
                                await _mediator.Send(new CreateOrderStatusByStatusIdQueryReq(OrderStatusType.CancellationOrderPayment.ToString(), order.OrderId, order.UserId));

                                return Response;
                            }

                            WalletTransactionDto transaction2 = new WalletTransactionDto()
                            {
                                WalletId = wallet.WalletId,
                                Description = $"تسویه صورتحساب سفارش {order.OrderCode} ",
                                Type = "Amount",
                                TypeId = 2,
                                Price = Convert.ToInt32(order.Total),
                            };
                            var createdTransaction2 = await _mediator.Send(new CreateWalletTransactionCommandReq(transaction2, false));
                            //ثیت وضعیت
                            await _mediator.Send(new CreateOrderStatusByStatusIdQueryReq(OrderStatusType.OrderDoing.ToString(), order.OrderId, order.UserId));

                            Response = await _mediator.Send(new CallBackCompleteOrderQueryReq(order.OrderId, createdTransaction2.walletTransaction.Id, null));
                            break;

                        //از تسهیلات
                        case 3:
                            //var walletFecilities = await _walletRep.GetRemainingOfWallet(order.UserId, "Fecilities");
                            if (order.Total > wallet.Fecilities)
                            {
                                Response.IsSuccessed = false;
                                Response.Message = "موجودی تسهیلات شما جهت تسویه سفارش کافی نیست ";
                                //ثیت وضعیت
                                await _mediator.Send(new CreateOrderStatusByStatusIdQueryReq(OrderStatusType.CancellationOrderPayment.ToString(), order.OrderId, order.UserId));

                                return Response;
                            }
                            await _mediator.Send(new CreateAghsatQueryReq(order, request.transactionId.Value, request.month.Value));
                            WalletTransactionDto transaction3 = new WalletTransactionDto()
                            {
                                WalletId = wallet.WalletId,
                                Description = $"تسویه صورتحساب سفارش {order.OrderCode} ",
                                Type = "Fecilities",
                                TypeId = 2,
                                Price = Convert.ToInt32(order.Total),
                            };
                            var createdTransaction3 = await _mediator.Send(new CreateWalletTransactionCommandReq(transaction3, false));
                            Response = await _mediator.Send(new CallBackCompleteOrderQueryReq(order.OrderId, createdTransaction3.walletTransaction.Id, null));

                           

                            WalletTransactionDto ExpireTransaction = new WalletTransactionDto()
                            {
                                WalletId = wallet.WalletId,
                                Description = "ابطال تسهیلات",
                                Type = "Fecilities",
                                TypeId = 2,
                                Price = Convert.ToInt32(wallet.Fecilities),
                            };
                            var createdExpireTransaction = await _mediator.Send(new CreateWalletTransactionCommandReq(ExpireTransaction, true));


                            //ثیت وضعیت
                            await _mediator.Send(new CreateOrderStatusByStatusIdQueryReq(OrderStatusType.OrderDoing.ToString(), order.OrderId, order.UserId));

                            break;
                        //اعتبار سازمانی
                        case 4:
                            //var walletOrgCredit = await _walletRep.GetRemainingOfWallet(order.UserId, "Fecilities");
                            if (order.Total > wallet.OrgCredit)
                            {
                                Response.IsSuccessed = false;
                                Response.Message = "موجودی اعتبار سازمانی شما جهت تسویه سفارش کافی نیست ";
                                //ثیت وضعیت
                                await _mediator.Send(new CreateOrderStatusByStatusIdQueryReq(OrderStatusType.CancellationOrderPayment.ToString(), order.OrderId, order.UserId));

                                return Response;
                            }

                            //var ApiOrderSale = await _mediator.Send(new SendCreditOfUserOrderQueryReq(order.OrderId));
                            //if (!ApiOrderSale)
                            //{
                            //    Response.IsSuccessed = false;
                            //    Response.Message = "سرور استعلام اعتبار سازمانی قادر به ثبت سفارش شما نمی باشد.لطفا از روش دیگیری برای پرداخت استفاده نمایید ";
                            //    //ثبت وضعیت
                            //    await _mediator.Send(new CreateOrderStatusByStatusIdQueryReq(OrderStatusType.CancellationOrderPayment.ToString(), order.OrderId, order.UserId));

                            //    return Response;
                            //}


                            await _mediator.Send(new CreateAghsatQueryReq(order, request.transactionId.Value, request.month.Value));
                            WalletTransactionDto transaction4 = new WalletTransactionDto()
                            {
                                WalletId = wallet.WalletId,
                                Description = $"تسویه صورتحساب سفارش {order.OrderCode} ",
                                Type = "OrgCredit",
                                TypeId = 2,
                                Price = Convert.ToInt32(order.Total),
                            };
                            var createdTransaction4 = await _mediator.Send(new CreateWalletTransactionCommandReq(transaction4, false));
                            Response = await _mediator.Send(new CallBackCompleteOrderQueryReq(order.OrderId, createdTransaction4.walletTransaction.Id, null));

                            
                            //ثیت وضعیت
                            await _mediator.Send(new CreateOrderStatusByStatusIdQueryReq(OrderStatusType.OrderDoing.ToString(), order.OrderId, order.UserId));

                            break;
                        //case 5:
                        //    WalletTransactionDto transaction5 = new WalletTransactionDto()
                        //    {
                        //        WalletId = wallet.WalletId,
                        //        CreateDate = DateTime.Now,
                        //        Description = order.OrderCode,
                        //        Type = "Amount",
                        //        TypeId = 3,
                        //        Price = Convert.ToInt32(order.Total),
                        //    };
                        //    //ثیت وضعیت
                        //    await _mediator.Send(new CreateOrderStatusByStatusIdQueryReq(OrderStatusType.OrderAwaitingPayment.ToString(), order.OrderId, order.UserId));


                        //    var createdTransaction5 = await _mediator.Send(new CreateWalletTransactionCommandReq(transaction5, false));
                        //    Response.IsSuccessed = true;
                        //    Response.Message = "در حال انتقال به درگاه پرداخت";




                        //    Response.Object = await _mediator.Send(new ZarrinPalQueryReq(order.OrderCode, createdTransaction5.walletTransaction.Id));

                        //    Response.IsSuccessed = true;
                        //    //ثیت وضعیت
                        //    await _mediator.Send(new CreateOrderStatusByStatusIdQueryReq(OrderStatusType.OrderAwaitingPayment.ToString(), order.OrderId, order.UserId));


                        //    return Response;
                        //    break;

                        //درگاه پرداخت نوپی
                        case 6:
                            WalletTransactionDto transaction6 = new WalletTransactionDto()
                            {
                                WalletId = wallet.WalletId,
                                CreateDate = DateTime.Now,
                                Description = order.OrderCode,
                                Type = "Amount",
                                TypeId = 3,
                                Price = Convert.ToInt32(item.Price),
                            };
                            //ثیت وضعیت
                            await _mediator.Send(new CreateOrderStatusByStatusIdQueryReq(OrderStatusType.OrderAwaitingPayment.ToString(), order.OrderId, order.UserId));


                            var createdTransaction6 = await _mediator.Send(new CreateWalletTransactionCommandReq(transaction6, false));
                            Response.IsSuccessed = true;
                            Response.Message = "در حال انتقال به درگاه پرداخت";



                            Response.Object = await _mediator.Send(new NoPayQueryReq(order.OrderCode, createdTransaction6.walletTransaction.Id, item.Price));

                            Response.IsSuccessed = true;
                            //ثیت وضعیت
                            await _mediator.Send(new CreateOrderStatusByStatusIdQueryReq(OrderStatusType.OrderAwaitingPayment.ToString(), order.OrderId, order.UserId));


                            return Response;
                    }
                }
            }
            //اگر بیش از یک حالت بود و بخشی از پول رو از حساب و بخش دیگه رو از درگاه خواست پرداخت کنه
            else
            {
                if (res.orderPayResult.Any(u => u.PayTypeId == 5))
                {
					
					var secoundOrderPay = res.orderPayResult.FirstOrDefault(u => u.PayTypeId != 5);
					await _mediator.Send(new CompletaOrderWithoutDargahAndStatusAndCallBackQueryReq(order.OrderId, secoundOrderPay.PayTypeId, secoundOrderPay.Price));
					var Result = await _mediator.Send(new CallBackCompleteOrderQueryReq(order.OrderId, 0, "CashPay".ToString()));


					var redirect = $"https://localhost:7040/checkout/payment/Ok";
				}
                else
                {
					//پرداخت الکتورنیکی
					var dargaPay = res.orderPayResult.FirstOrDefault(u => u.PayTypeId == 1);
					WalletTransactionDto transaction1 = new WalletTransactionDto()
					{
						WalletId = wallet.WalletId,
						CreateDate = DateTime.Now,
						Description = order.OrderCode,
						Type = "Amount",
						TypeId = 3,
						Price = Convert.ToInt32(dargaPay.Price),
					};
					//ثیت وضعیت
					await _mediator.Send(new CreateOrderStatusByStatusIdQueryReq(OrderStatusType.OrderAwaitingPayment.ToString(), order.OrderId, order.UserId));


					var createdTransaction1 = await _mediator.Send(new CreateWalletTransactionCommandReq(transaction1, false));
					Response.IsSuccessed = true;
					Response.Message = "مبلغ ما به التفاوت سفارش در حال انتقال به درگاه میباشد";



					Response.Object = await _mediator.Send(new DargaQueryReq(order.OrderCode, createdTransaction1.walletTransaction.Id, dargaPay.Price));

					Response.IsSuccessed = true;
					//ثیت وضعیت
					await _mediator.Send(new CreateOrderStatusByStatusIdQueryReq(OrderStatusType.OrderAwaitingPayment.ToString(), order.OrderId, order.UserId));


					string insertQuery = $"INSERT INTO dbo.SecoundPayAfterDargah(orderId, transactionId, month) VALUES ({order.OrderId}, {request.transactionId}, {request.month})";

					DapperHelper.ExecuteCommand(_connectionString, conn => conn.Query(insertQuery).ToList());
					return Response;
				}
                

				
			}


            order.CreateDate = DateTime.Now;
            await _orderRep.UpdateAsync(order);
            return Response;
        }
    }
}
