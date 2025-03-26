using Dapper;

using MediatR;

using Microsoft.Extensions.Configuration;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Dapper.Helper;
using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.DTOs.WalletTransaction;
using Parstech.Shop.Context.Application.Enum;
using Parstech.Shop.Context.Application.Features.Order.Requests.Queries;
using Parstech.Shop.Context.Application.Features.OrderStatus.Requests.Queries;
using Parstech.Shop.Context.Application.Features.WalletTransaction.Requests.Commands;
using Parstech.Shop.Context.Application.Features.WalletTransaction.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.Order.Handler.Queries;

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


    public async Task<ResponseDto> Handle(CompleteOrderByAdminQueryReq request, CancellationToken cancellationToken)
    {

        ResponseDto Response = new();
        if (request.orderId == 0)
        {
            Response.IsSuccessed = false;
            Response.Message = "سفارش معتبر نمی باشد";
            return Response;
        }

        var order = await _orderRep.GetAsync(request.orderId);

        var orderCuopon = await _orderCouponRep.GetByOrderId(order.OrderId);
        var wallet = await _walletRep.GetWalletByUserId(order.UserId);


        if (order.IsFinaly)
        {
            Response.IsSuccessed = false;
            Response.Message = "تکمیل پرداخت تنها برای سبد خرید امکان پذیر است ";
            return Response;
        }
        var user = await _userRep.GetAsync(order.UserId);
        var userBilling = await _userBillingRep.GetUserBillingByUserId(order.UserId);
        #region OrderPay

        var res = await _orderPayRep.GetListByOrderId(order.OrderId);
        #endregion



        //اگر نرمال بود و فقط یک حالت پرداخت داشت
        if (res.Count == 1)
        {
            foreach (var item in res)
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
                            Price = Convert.ToInt32(item.Price),
                        };
                        //ثیت وضعیت
                        await _mediator.Send(new CreateOrderStatusByStatusIdQueryReq(OrderStatusType.OrderAwaitingPayment.ToString(), order.OrderId, order.UserId));


                        var createdTransaction1 = await _mediator.Send(new CreateWalletTransactionCommandReq(transaction1, false));
                        Response.IsSuccessed = true;
                        Response.Message = "در حال انتقال به درگاه پرداخت";



                        Response.Object = await _mediator.Send(new DargaQueryReq(order.OrderCode, createdTransaction1.walletTransaction.Id, item.Price));

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

                        WalletTransactionDto transaction2 = new()
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
                        Response.IsSuccessed = true;
                        Response.Message = "سفارش با موفقیت پرداخت شد";
                        break;

                    //از تسهیلات
                    case 3:
                        var activeTransaction3 = await _mediator.Send(new GetActiveAghsatTransactionQueryReq(wallet.WalletId, request.typeName));
                        if (activeTransaction3 == null)
                        {
                            Response.IsSuccessed = false;
                            Response.Message = "اعتبار فعالی یافت نشد";
                            return Response;
                        }
                        //var walletFecilities = await _walletRep.GetRemainingOfWallet(order.UserId, "Fecilities");
                        if (order.Total > wallet.Fecilities)
                        {
                            Response.IsSuccessed = false;
                            Response.Message = "موجودی تسهیلات شما جهت تسویه سفارش کافی نیست ";
                            //ثیت وضعیت
                            await _mediator.Send(new CreateOrderStatusByStatusIdQueryReq(OrderStatusType.CancellationOrderPayment.ToString(), order.OrderId, order.UserId));

                            return Response;
                        }
                        await _mediator.Send(new CreateAghsatQueryReq(order, activeTransaction3.Id, request.month.Value));
                        WalletTransactionDto transaction3 = new()
                        {
                            WalletId = wallet.WalletId,
                            Description = $"تسویه صورتحساب سفارش {order.OrderCode} ",
                            Type = "Fecilities",
                            TypeId = 2,
                            Price = Convert.ToInt32(order.Total),
                        };
                        var createdTransaction3 = await _mediator.Send(new CreateWalletTransactionCommandReq(transaction3, false));
                        Response = await _mediator.Send(new CallBackCompleteOrderQueryReq(order.OrderId, createdTransaction3.walletTransaction.Id, null));



                        WalletTransactionDto ExpireTransaction = new()
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
                        Response.IsSuccessed = true;
                        Response.Message = "سفارش با موفقیت پرداخت شد";
                        break;
                    //اعتبار سازمانی
                    case 4:
                        var activeTransaction4 = await _mediator.Send(new GetActiveAghsatTransactionQueryReq(wallet.WalletId, request.typeName));
                        if (activeTransaction4 == null)
                        {
                            Response.IsSuccessed = false;
                            Response.Message = "اعتبار فعالی یافت نشد";
                            return Response;
                        }
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


                        await _mediator.Send(new CreateAghsatQueryReq(order, activeTransaction4.Id, request.month.Value));
                        WalletTransactionDto transaction4 = new()
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
                        Response.IsSuccessed = true;
                        Response.Message = "سفارش با موفقیت پرداخت شد";
                        break;
                    case 5:

                        var orderPay = res.FirstOrDefault(u => u.PayTypeId == 5);
                        await _mediator.Send(new CompletaOrderWithoutDargahAndStatusAndCallBackQueryReq(order.OrderId, 5, orderPay.Price));
                        var Result = await _mediator.Send(new CallBackCompleteOrderQueryReq(order.OrderId, 0, "CashPay".ToString()));

                        Response.IsSuccessed = true;
                        Response.Message = "سفارش با موفقیت پرداخت شد";
                        Response.IsSuccessed = true;
                        //ثیت وضعیت
                        await _mediator.Send(new CreateOrderStatusByStatusIdQueryReq(OrderStatusType.OrderDoing.ToString(), order.OrderId, order.UserId));



                        break;
                }
            }
        }
        //اگر بیش از یک حالت بود و بخشی از پول رو از حساب و بخش دیگه رو از درگاه خواست پرداخت کنه
        else
        {
            if (res.Any(u => u.PayTypeId == 5))
            {
                var activeTransaction5 = await _mediator.Send(new GetActiveAghsatTransactionQueryReq(wallet.WalletId, request.typeName));
                if (activeTransaction5 == null)
                {
                    Response.IsSuccessed = false;
                    Response.Message = "اعتبار فعالی یافت نشد";
                    return Response;
                }
                var secoundOrderPay = res.FirstOrDefault(u => u.PayTypeId != 5);
                string insertQuery = $"INSERT INTO dbo.SecoundPayAfterDargah(orderId, transactionId, month) VALUES ({order.OrderId}, {activeTransaction5.Id}, {request.month})";

                DapperHelper.ExecuteCommand(_connectionString, conn => conn.Query(insertQuery).ToList());
                await _mediator.Send(new CompletaOrderWithoutDargahAndStatusAndCallBackQueryReq(order.OrderId, secoundOrderPay.PayTypeId, secoundOrderPay.Price));
                var Result = await _mediator.Send(new CallBackCompleteOrderQueryReq(order.OrderId, 0, "CashPay".ToString()));

                Response.IsSuccessed = true;
                Response.Message = "سفارش با موفقیت پرداخت شد";
                //var redirect = $"https://localhost:7040/checkout/payment/Ok";
            }
            else
            {
                Response.IsSuccessed = false;
                Response.Message = "درگاه در پنل ادمین غیر فعال است";
                return Response;
            }



        }


        order.CreateDate = DateTime.Now;
        await _orderRep.UpdateAsync(order);
        return Response;
    }



}