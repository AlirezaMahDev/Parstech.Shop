using Dapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.Enum;
using Parstech.Shop.ApiService.Application.Features.Order.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.OrderPay.Request.Queries;
using Parstech.Shop.ApiService.Application.Features.OrderStatus.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.WalletTransaction.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.WalletTransaction.Requests.Queries;
using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Features.Order.Handler.Queries;

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
        ISecoundPayAfterDargahRepository secoundPayAfterDargahRep,
        IConfiguration configuration)
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
        ResponseDto response = new();
        if (request.orderId == 0)
        {
            response.IsSuccessed = false;
            response.Message = "سفارش معتبر نمی باشد";
            return response;
        }

        if (request.orderShippingId == 0)
        {
            response.IsSuccessed = false;
            response.Message = "ابتدا آدرسی برای سفارش خود ثبت و مجددا امتحان کنید";
            return response;
        }

        if (request.payTypeId == 0)
        {
            response.IsSuccessed = false;
            response.Message = "روش پرداخت سفارش را انتخاب نمایید";
            return response;
        }

        Shared.Models.Order? order = await _orderRep.GetAsync(request.orderId);
        Shared.Models.OrderShipping? orderShipping = await _orderShippingRep.GetAsync(request.orderShippingId);
        Shared.Models.PayType? payType = await _payRep.GetAsync(request.payTypeId);
        OrderCoupon? orderCuopon = await _orderCouponRep.GetByOrderId(order.OrderId);
        Shared.Models.Wallet wallet = await _walletRep.GetWalletByUserId(order.UserId);
        Shared.Models.User? user = await _userRep.GetAsync(order.UserId);
        Shared.Models.UserBilling? userBilling = await _userBillingRep.GetUserBillingByUserId(order.UserId);

        #region OrderPay

        var res = await _mediator.Send(new ChoisePayTypeForCreateOrderPayQueryReq(payType.Id, wallet, order));

        #endregion

        //اگر مشکل داشت
        if (!res.IsSuccessed)
        {
            response.IsSuccessed = false;
            response.Message = res.Message;
            return response;
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
                        WalletTransactionDto transaction1 = new()
                        {
                            WalletId = wallet.WalletId,
                            CreateDate = DateTime.Now,
                            Description = order.OrderCode,
                            Type = "Amount",
                            TypeId = 3,
                            Price = Convert.ToInt32(item.Price)
                        };
                        //ثیت وضعیت
                        await _mediator.Send(new CreateOrderStatusByStatusIdQueryReq(
                            OrderStatusType.OrderAwaitingPayment.ToString(),
                            order.OrderId,
                            order.UserId));


                        var createdTransaction1 =
                            await _mediator.Send(new CreateWalletTransactionCommandReq(transaction1, false));
                        response.IsSuccessed = true;
                        response.Message = "در حال انتقال به درگاه پرداخت";


                        response.Object = await _mediator.Send(new DargaQueryReq(order.OrderCode,
                            createdTransaction1.walletTransaction.Id,
                            item.Price));

                        response.IsSuccessed = true;
                        //ثیت وضعیت
                        await _mediator.Send(new CreateOrderStatusByStatusIdQueryReq(
                            OrderStatusType.OrderAwaitingPayment.ToString(),
                            order.OrderId,
                            order.UserId));


                        return response;

                    //از کیف پول
                    case 2:

                        if (order.Total > wallet.Amount)
                        {
                            response.IsSuccessed = false;
                            response.Message = "موجودی حساب شما جهت تسویه سفارش کافی نیست ";
                            //ثیت وضعیت
                            await _mediator.Send(new CreateOrderStatusByStatusIdQueryReq(
                                OrderStatusType.CancellationOrderPayment.ToString(),
                                order.OrderId,
                                order.UserId));

                            return response;
                        }

                        WalletTransactionDto transaction2 = new()
                        {
                            WalletId = wallet.WalletId,
                            Description = $"تسویه صورتحساب سفارش {order.OrderCode} ",
                            Type = "Amount",
                            TypeId = 2,
                            Price = Convert.ToInt32(order.Total)
                        };
                        var createdTransaction2 =
                            await _mediator.Send(new CreateWalletTransactionCommandReq(transaction2, false));
                        //ثیت وضعیت
                        await _mediator.Send(
                            new CreateOrderStatusByStatusIdQueryReq(OrderStatusType.OrderDoing.ToString(),
                                order.OrderId,
                                order.UserId));

                        response = await _mediator.Send(new CallBackCompleteOrderQueryReq(order.OrderId,
                            createdTransaction2.walletTransaction.Id,
                            null));
                        break;

                    //از تسهیلات
                    case 3:
                        //var walletFecilities = await _walletRep.GetRemainingOfWallet(order.UserId, "Fecilities");
                        if (order.Total > wallet.Fecilities)
                        {
                            response.IsSuccessed = false;
                            response.Message = "موجودی تسهیلات شما جهت تسویه سفارش کافی نیست ";
                            //ثیت وضعیت
                            await _mediator.Send(new CreateOrderStatusByStatusIdQueryReq(
                                OrderStatusType.CancellationOrderPayment.ToString(),
                                order.OrderId,
                                order.UserId));

                            return response;
                        }

                        await _mediator.Send(new CreateAghsatQueryReq(order,
                            request.transactionId.Value,
                            request.month.Value));
                        WalletTransactionDto transaction3 = new()
                        {
                            WalletId = wallet.WalletId,
                            Description = $"تسویه صورتحساب سفارش {order.OrderCode} ",
                            Type = "Fecilities",
                            TypeId = 2,
                            Price = Convert.ToInt32(order.Total)
                        };
                        var createdTransaction3 =
                            await _mediator.Send(new CreateWalletTransactionCommandReq(transaction3, false));
                        response = await _mediator.Send(new CallBackCompleteOrderQueryReq(order.OrderId,
                            createdTransaction3.walletTransaction.Id,
                            null));


                        WalletTransactionDto expireTransaction = new()
                        {
                            WalletId = wallet.WalletId,
                            Description = "ابطال تسهیلات",
                            Type = "Fecilities",
                            TypeId = 2,
                            Price = Convert.ToInt32(wallet.Fecilities)
                        };
                        var createdExpireTransaction =
                            await _mediator.Send(new CreateWalletTransactionCommandReq(expireTransaction, true));


                        //ثیت وضعیت
                        await _mediator.Send(
                            new CreateOrderStatusByStatusIdQueryReq(OrderStatusType.OrderDoing.ToString(),
                                order.OrderId,
                                order.UserId));

                        break;
                    //اعتبار سازمانی
                    case 4:
                        //var walletOrgCredit = await _walletRep.GetRemainingOfWallet(order.UserId, "Fecilities");
                        if (order.Total > wallet.OrgCredit)
                        {
                            response.IsSuccessed = false;
                            response.Message = "موجودی اعتبار سازمانی شما جهت تسویه سفارش کافی نیست ";
                            //ثیت وضعیت
                            await _mediator.Send(new CreateOrderStatusByStatusIdQueryReq(
                                OrderStatusType.CancellationOrderPayment.ToString(),
                                order.OrderId,
                                order.UserId));

                            return response;
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


                        await _mediator.Send(new CreateAghsatQueryReq(order,
                            request.transactionId.Value,
                            request.month.Value));
                        WalletTransactionDto transaction4 = new()
                        {
                            WalletId = wallet.WalletId,
                            Description = $"تسویه صورتحساب سفارش {order.OrderCode} ",
                            Type = "OrgCredit",
                            TypeId = 2,
                            Price = Convert.ToInt32(order.Total)
                        };
                        var createdTransaction4 =
                            await _mediator.Send(new CreateWalletTransactionCommandReq(transaction4, false));
                        response = await _mediator.Send(new CallBackCompleteOrderQueryReq(order.OrderId,
                            createdTransaction4.walletTransaction.Id,
                            null));


                        //ثیت وضعیت
                        await _mediator.Send(
                            new CreateOrderStatusByStatusIdQueryReq(OrderStatusType.OrderDoing.ToString(),
                                order.OrderId,
                                order.UserId));

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
                        WalletTransactionDto transaction6 = new()
                        {
                            WalletId = wallet.WalletId,
                            CreateDate = DateTime.Now,
                            Description = order.OrderCode,
                            Type = "Amount",
                            TypeId = 3,
                            Price = Convert.ToInt32(item.Price)
                        };
                        //ثیت وضعیت
                        await _mediator.Send(new CreateOrderStatusByStatusIdQueryReq(
                            OrderStatusType.OrderAwaitingPayment.ToString(),
                            order.OrderId,
                            order.UserId));


                        var createdTransaction6 =
                            await _mediator.Send(new CreateWalletTransactionCommandReq(transaction6, false));
                        response.IsSuccessed = true;
                        response.Message = "در حال انتقال به درگاه پرداخت";


                        response.Object = await _mediator.Send(new NoPayQueryReq(order.OrderCode,
                            createdTransaction6.walletTransaction.Id,
                            item.Price));

                        response.IsSuccessed = true;
                        //ثیت وضعیت
                        await _mediator.Send(new CreateOrderStatusByStatusIdQueryReq(
                            OrderStatusType.OrderAwaitingPayment.ToString(),
                            order.OrderId,
                            order.UserId));


                        return response;
                }
            }
        }
        //اگر بیش از یک حالت بود و بخشی از پول رو از حساب و بخش دیگه رو از درگاه خواست پرداخت کنه
        else
        {
            if (res.orderPayResult.Any(u => u.PayTypeId == 5))
            {
                var secoundOrderPay = res.orderPayResult.FirstOrDefault(u => u.PayTypeId != 5);
                await _mediator.Send(new CompletaOrderWithoutDargahAndStatusAndCallBackQueryReq(order.OrderId,
                    secoundOrderPay.PayTypeId,
                    secoundOrderPay.Price));
                ResponseDto result =
                    await _mediator.Send(new CallBackCompleteOrderQueryReq(order.OrderId, 0, "CashPay".ToString()));


                string redirect = $"https://localhost:7040/checkout/payment/Ok";
            }
            else
            {
                //پرداخت الکتورنیکی
                var dargaPay = res.orderPayResult.FirstOrDefault(u => u.PayTypeId == 1);
                WalletTransactionDto transaction1 = new()
                {
                    WalletId = wallet.WalletId,
                    CreateDate = DateTime.Now,
                    Description = order.OrderCode,
                    Type = "Amount",
                    TypeId = 3,
                    Price = Convert.ToInt32(dargaPay.Price)
                };
                //ثیت وضعیت
                await _mediator.Send(new CreateOrderStatusByStatusIdQueryReq(
                    OrderStatusType.OrderAwaitingPayment.ToString(),
                    order.OrderId,
                    order.UserId));


                var createdTransaction1 =
                    await _mediator.Send(new CreateWalletTransactionCommandReq(transaction1, false));
                response.IsSuccessed = true;
                response.Message = "مبلغ ما به التفاوت سفارش در حال انتقال به درگاه میباشد";


                response.Object = await _mediator.Send(new DargaQueryReq(order.OrderCode,
                    createdTransaction1.walletTransaction.Id,
                    dargaPay.Price));

                response.IsSuccessed = true;
                //ثیت وضعیت
                await _mediator.Send(new CreateOrderStatusByStatusIdQueryReq(
                    OrderStatusType.OrderAwaitingPayment.ToString(),
                    order.OrderId,
                    order.UserId));


                string insertQuery =
                    $"INSERT INTO dbo.SecoundPayAfterDargah(orderId, transactionId, month) VALUES ({order.OrderId}, {request.transactionId}, {request.month})";

                DapperHelper.ExecuteCommand(_connectionString, conn => conn.Query(insertQuery).ToList());
                return response;
            }
        }


        order.CreateDate = DateTime.Now;
        await _orderRep.UpdateAsync(order);
        return response;
    }
}