using Dapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.Enum;
using Parstech.Shop.ApiService.Application.Features.Order.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.OrderStatus.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.WalletTransaction.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.WalletTransaction.Requests.Queries;
using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Features.Order.Handler.Queries;

public class CompleteOrderByAdminQueryHandler : IRequestHandler<CompleteOrderByAdminQueryReq, ResponseDto>
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

    public CompleteOrderByAdminQueryHandler(IMediator mediator,
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


    public async Task<ResponseDto> Handle(CompleteOrderByAdminQueryReq request, CancellationToken cancellationToken)
    {
        ResponseDto response = new();
        if (request.orderId == 0)
        {
            response.IsSuccessed = false;
            response.Message = "سفارش معتبر نمی باشد";
            return response;
        }

        Shared.Models.Order? order = await _orderRep.GetAsync(request.orderId);

        OrderCoupon? orderCuopon = await _orderCouponRep.GetByOrderId(order.OrderId);
        Shared.Models.Wallet wallet = await _walletRep.GetWalletByUserId(order.UserId);


        if (order.IsFinaly)
        {
            response.IsSuccessed = false;
            response.Message = "تکمیل پرداخت تنها برای سبد خرید امکان پذیر است ";
            return response;
        }

        Shared.Models.User? user = await _userRep.GetAsync(order.UserId);
        Shared.Models.UserBilling? userBilling = await _userBillingRep.GetUserBillingByUserId(order.UserId);

        #region OrderPay

        List<Shared.Models.OrderPay> res = await _orderPayRep.GetListByOrderId(order.OrderId);

        #endregion


        //اگر نرمال بود و فقط یک حالت پرداخت داشت
        if (res.Count == 1)
        {
            foreach (Shared.Models.OrderPay item in res)
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
                        response.IsSuccessed = true;
                        response.Message = "سفارش با موفقیت پرداخت شد";
                        break;

                    //از تسهیلات
                    case 3:
                        var activeTransaction3 =
                            await _mediator.Send(
                                new GetActiveAghsatTransactionQueryReq(wallet.WalletId, request.typeName));
                        if (activeTransaction3 == null)
                        {
                            response.IsSuccessed = false;
                            response.Message = "اعتبار فعالی یافت نشد";
                            return response;
                        }

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

                        await _mediator.Send(
                            new CreateAghsatQueryReq(order, activeTransaction3.Id, request.month.Value));
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
                        response.IsSuccessed = true;
                        response.Message = "سفارش با موفقیت پرداخت شد";
                        break;
                    //اعتبار سازمانی
                    case 4:
                        var activeTransaction4 =
                            await _mediator.Send(
                                new GetActiveAghsatTransactionQueryReq(wallet.WalletId, request.typeName));
                        if (activeTransaction4 == null)
                        {
                            response.IsSuccessed = false;
                            response.Message = "اعتبار فعالی یافت نشد";
                            return response;
                        }

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


                        await _mediator.Send(
                            new CreateAghsatQueryReq(order, activeTransaction4.Id, request.month.Value));
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
                        response.IsSuccessed = true;
                        response.Message = "سفارش با موفقیت پرداخت شد";
                        break;
                    case 5:

                        Shared.Models.OrderPay? orderPay = res.FirstOrDefault(u => u.PayTypeId == 5);
                        await _mediator.Send(
                            new CompletaOrderWithoutDargahAndStatusAndCallBackQueryReq(order.OrderId,
                                5,
                                orderPay.Price));
                        ResponseDto result =
                            await _mediator.Send(
                                new CallBackCompleteOrderQueryReq(order.OrderId, 0, "CashPay".ToString()));

                        response.IsSuccessed = true;
                        response.Message = "سفارش با موفقیت پرداخت شد";
                        response.IsSuccessed = true;
                        //ثیت وضعیت
                        await _mediator.Send(
                            new CreateOrderStatusByStatusIdQueryReq(OrderStatusType.OrderDoing.ToString(),
                                order.OrderId,
                                order.UserId));


                        break;
                }
            }
        }
        //اگر بیش از یک حالت بود و بخشی از پول رو از حساب و بخش دیگه رو از درگاه خواست پرداخت کنه
        else
        {
            if (res.Any(u => u.PayTypeId == 5))
            {
                var activeTransaction5 =
                    await _mediator.Send(new GetActiveAghsatTransactionQueryReq(wallet.WalletId, request.typeName));
                if (activeTransaction5 == null)
                {
                    response.IsSuccessed = false;
                    response.Message = "اعتبار فعالی یافت نشد";
                    return response;
                }

                Shared.Models.OrderPay? secoundOrderPay = res.FirstOrDefault(u => u.PayTypeId != 5);
                string insertQuery =
                    $"INSERT INTO dbo.SecoundPayAfterDargah(orderId, transactionId, month) VALUES ({order.OrderId}, {activeTransaction5.Id}, {request.month})";

                DapperHelper.ExecuteCommand(_connectionString, conn => conn.Query(insertQuery).ToList());
                await _mediator.Send(new CompletaOrderWithoutDargahAndStatusAndCallBackQueryReq(order.OrderId,
                    secoundOrderPay.PayTypeId,
                    secoundOrderPay.Price));
                ResponseDto result =
                    await _mediator.Send(new CallBackCompleteOrderQueryReq(order.OrderId, 0, "CashPay".ToString()));

                response.IsSuccessed = true;
                response.Message = "سفارش با موفقیت پرداخت شد";
                //var redirect = $"https://localhost:7040/checkout/payment/Ok";
            }
            else
            {
                response.IsSuccessed = false;
                response.Message = "درگاه در پنل ادمین غیر فعال است";
                return response;
            }
        }


        order.CreateDate = DateTime.Now;
        await _orderRep.UpdateAsync(order);
        return response;
    }
}