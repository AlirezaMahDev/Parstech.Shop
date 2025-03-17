using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Order.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.WalletTransaction.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.WalletTransaction.Requests.Queries;
using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Application.Features.Order.Handler.Queries;

public class
    CompletaOrderWithoutDargahAndStatusAndCallBackQueryHandler : IRequestHandler<
    CompletaOrderWithoutDargahAndStatusAndCallBackQueryReq, ResponseDto>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
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

    public CompletaOrderWithoutDargahAndStatusAndCallBackQueryHandler(IMediator mediator,
        IMapper mapper,
        IOrderRepository orderRep,
        IOrderPayRepository orderPayRep,
        IOrderShippingRepository orderShippingRep,
        IPayTypeRepository payRep,
        IUserRepository userRep,
        IWalletRepository walletRep,
        IWalletTransactionRepository walletTransactionRep,
        IOrderCouponRepository orderCouponRep,
        IUserBillingRepository userBillingRep,
        ISecoundPayAfterDargahRepository secoundPayAfterDargahRep)
    {
        _mediator = mediator;
        _mapper = mapper;
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
    }

    public async Task<ResponseDto> Handle(CompletaOrderWithoutDargahAndStatusAndCallBackQueryReq request,
        CancellationToken cancellationToken)
    {
        ResponseDto Response = new();
        if (request.orderId == 0)
        {
            Response.IsSuccessed = false;
            Response.Message = "سفارش معتبر نمی باشد";
            return Response;
        }

        if (request.payTypeId == 0)
        {
            Response.IsSuccessed = false;
            Response.Message = "روش پرداخت سفارش را انتخاب نمایید";
            return Response;
        }

        Domain.Models.Order? order = await _orderRep.GetAsync(request.orderId);

        Domain.Models.PayType? PayType = await _payRep.GetAsync(request.payTypeId);
        OrderCoupon? orderCuopon = await _orderCouponRep.GetByOrderId(order.OrderId);
        Domain.Models.Wallet wallet = await _walletRep.GetWalletByUserId(order.UserId);
        Domain.Models.User? user = await _userRep.GetAsync(order.UserId);
        Domain.Models.UserBilling? userBilling = await _userBillingRep.GetUserBillingByUserId(order.UserId);

        #region OrderPay

        List<Domain.Models.OrderPay> res = await _orderPayRep.GetListByOrderId(order.OrderId);
        //var res = await _mediator.Send(new ChoisePayTypeForCreateOrderPayQueryReq(PayType.Id, wallet, order));

        #endregion

        foreach (Domain.Models.OrderPay item in res)
        {
            switch (item.PayTypeId)
            {
                //از کیف پول
                case 2:


                    WalletTransactionDto transaction2 = new()
                    {
                        WalletId = wallet.WalletId,
                        Description = $"تسویه صورتحساب سفارش {order.OrderCode} ",
                        Type = "Amount",
                        TypeId = 2,
                        Price = Convert.ToInt32(item.Price)
                    };
                    void createdTransaction2 =
                        await _mediator.Send(new CreateWalletTransactionCommandReq(transaction2, false));

                    break;

                //از تسهیلات
                case 3:
                    SecoundPayAfterDargah secoundPay = await _secoundPayAfterDargahRep.GetByOrderId(order.OrderId);
                    await _mediator.Send(new CreateAghsatQueryReq(order,
                        secoundPay.TransactionId.Value,
                        secoundPay.Month.Value));
                    WalletTransactionDto transaction3 = new()
                    {
                        WalletId = wallet.WalletId,
                        Description = $"تسویه صورتحساب سفارش {order.OrderCode} ",
                        Type = "Fecilities",
                        TypeId = 2,
                        Price = Convert.ToInt32(item.Price)
                    };
                    void createdTransaction3 =
                        await _mediator.Send(new CreateWalletTransactionCommandReq(transaction3, false));


                    Domain.Models.WalletTransaction? transaction =
                        await _walletTransactionRep.GetAsync(createdTransaction3.walletTransaction.Id);
                    WalletTransactionDto? transactionDto = _mapper.Map<WalletTransactionDto>(transaction);
                    await _walletRep.WalletCalculateTransaction(transactionDto);

                    WalletTransactionDto ExpireTransaction = new()
                    {
                        WalletId = wallet.WalletId,
                        Description = "ابطال تسهیلات",
                        Type = "Fecilities",
                        TypeId = 2,
                        Price = Convert.ToInt32(wallet.Fecilities)
                    };
                    void createdExpireTransaction =
                        await _mediator.Send(new CreateWalletTransactionCommandReq(ExpireTransaction, true));


                    break;
                //اعتبار سازمانی
                case 4:
                    SecoundPayAfterDargah secoundPay2 = await _secoundPayAfterDargahRep.GetByOrderId(order.OrderId);
                    await _mediator.Send(new CreateAghsatQueryReq(order,
                        secoundPay2.TransactionId.Value,
                        secoundPay2.Month.Value));
                    WalletTransactionDto transaction4 = new()
                    {
                        WalletId = wallet.WalletId,
                        Description = $"تسویه صورتحساب سفارش {order.OrderCode} ",
                        Type = "OrgCredit",
                        TypeId = 2,
                        Price = Convert.ToInt32(item.Price)
                    };
                    void createdTransaction4 =
                        await _mediator.Send(new CreateWalletTransactionCommandReq(transaction4, false));


                    Domain.Models.WalletTransaction? transactionO =
                        await _walletTransactionRep.GetAsync(createdTransaction4.walletTransaction.Id);
                    WalletTransactionDto? transactionDto2 = _mapper.Map<WalletTransactionDto>(transactionO);
                    await _walletRep.WalletCalculateTransaction(transactionDto2);
                    break;
            }
        }

        order.CreateDate = DateTime.Now;
        await _orderRep.UpdateAsync(order);
        return Response;
    }
}