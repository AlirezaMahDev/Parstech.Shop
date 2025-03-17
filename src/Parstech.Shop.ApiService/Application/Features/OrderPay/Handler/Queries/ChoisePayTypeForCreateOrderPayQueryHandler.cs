using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.OrderPay.Request.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.OrderPay.Handler.Queries;

public class
    ChoisePayTypeForCreateOrderPayQueryHandler : IRequestHandler<ChoisePayTypeForCreateOrderPayQueryReq,
    ResponseOrderPayDto>
{
    private readonly IOrderPayRepository _orderPayRep;

    public ChoisePayTypeForCreateOrderPayQueryHandler(IOrderPayRepository orderPayRep)
    {
        _orderPayRep = orderPayRep;
    }

    public async Task<ResponseOrderPayDto> Handle(ChoisePayTypeForCreateOrderPayQueryReq request,
        CancellationToken cancellationToken)
    {
        ResponseOrderPayDto response = new();
        List<Shared.Models.OrderPay> payResult = new();
        long walletPrice = 0;
        string walletdescription = "";
        long DargahPrice = 0;
        string Dargahdescription = "";

        if (await _orderPayRep.HasOrderPay(request.order.OrderId))
        {
            List<Shared.Models.OrderPay> items = await _orderPayRep.GetListByOrderId(request.order.OrderId);
            foreach (Shared.Models.OrderPay item in items)
            {
                await _orderPayRep.DeleteAsync(item);
            }
        }

        switch (request.payTypeId)
        {
            case 1:
                Dargahdescription = $"پرداخت  کل مبلغ سفارش از طریق درگاه پرداخت سداد {request.order.OrderCode}";
                Shared.Models.OrderPay res1 = await CreateOrderPay(request.order,
                    request.wallet,
                    request.payTypeId,
                    Dargahdescription,
                    request.order.Total);
                payResult.Add(res1);
                response.IsSuccessed = true;
                break;
            case 2:
                if (request.wallet.Amount == 0)
                {
                    response.Message = "موجودی حساب شما جهت تسویه سفارش کافی نیست";
                    response.IsSuccessed = false;
                }
                else if (request.order.Total <= request.wallet.Amount)
                {
                    walletdescription = $"پرداخت  کل مبلغ سفارش از کیف پول سفارش {request.order.OrderCode}";
                    Shared.Models.OrderPay res2 = await CreateOrderPay(request.order,
                        request.wallet,
                        request.payTypeId,
                        walletdescription,
                        request.order.Total);
                    payResult.Add(res2);
                    response.IsSuccessed = true;
                }
                else
                {
                    walletPrice = request.wallet.Amount;
                    walletdescription = $"پرداخت بخشی از مبلغ سفارش از کیف پول سفارش {request.order.OrderCode}";
                    DargahPrice = request.order.Total - request.wallet.Amount;
                    Dargahdescription = $"پرداخت ما به التفاوت از طریق درگاه پرداخت سداد {request.order.OrderCode}";
                    Shared.Models.OrderPay res21 = await CreateOrderPay(request.order,
                        request.wallet,
                        request.payTypeId,
                        walletdescription,
                        walletPrice);
                    payResult.Add(res21);
                    Shared.Models.OrderPay res22 = await CreateOrderPay(request.order,
                        request.wallet,
                        1,
                        Dargahdescription,
                        DargahPrice);
                    payResult.Add(res22);
                    response.IsSuccessed = true;
                }

                break;
            case 3:
                if (request.wallet.Fecilities == 0)
                {
                    response.Message = "موجودی حساب شما جهت تسویه سفارش کافی نیست";
                    response.IsSuccessed = false;
                }
                else if (request.order.Total <= request.wallet.Fecilities)
                {
                    walletdescription = $"پرداخت  کل مبلغ سفارش از تسهیلات سفارش {request.order.OrderCode}";
                    Shared.Models.OrderPay res2 = await CreateOrderPay(request.order,
                        request.wallet,
                        request.payTypeId,
                        walletdescription,
                        request.order.Total);
                    payResult.Add(res2);
                    response.IsSuccessed = true;
                }
                else
                {
                    walletPrice = request.wallet.Fecilities;
                    walletdescription = $"پرداخت بخشی از مبلغ سفارش از تسهیلات سفارش {request.order.OrderCode}";
                    DargahPrice = request.order.Total - request.wallet.Fecilities;
                    Dargahdescription = $"پرداخت ما به التفاوت از طریق درگاه پرداخت سداد {request.order.OrderCode}";
                    Shared.Models.OrderPay res21 = await CreateOrderPay(request.order,
                        request.wallet,
                        request.payTypeId,
                        walletdescription,
                        walletPrice);
                    payResult.Add(res21);
                    Shared.Models.OrderPay res22 = await CreateOrderPay(request.order,
                        request.wallet,
                        1,
                        Dargahdescription,
                        DargahPrice);
                    payResult.Add(res22);
                    response.IsSuccessed = true;
                }

                break;
            case 4:
                if (request.wallet.OrgCredit == 0)
                {
                    response.Message = "موجودی حساب شما جهت تسویه سفارش کافی نیست";
                    response.IsSuccessed = false;
                }
                else if (request.order.Total <= request.wallet.OrgCredit)
                {
                    walletdescription = $"پرداخت  کل مبلغ سفارش از اعتبار سازمانی سفارش {request.order.OrderCode}";
                    Shared.Models.OrderPay res2 = await CreateOrderPay(request.order,
                        request.wallet,
                        request.payTypeId,
                        walletdescription,
                        request.order.Total);
                    payResult.Add(res2);
                    response.IsSuccessed = true;
                }
                else
                {
                    walletPrice = request.wallet.OrgCredit;
                    walletdescription = $"پرداخت بخشی از مبلغ سفارش از اعتبار سازمانی سفارش {request.order.OrderCode}";
                    DargahPrice = request.order.Total - request.wallet.OrgCredit;
                    Dargahdescription = $"پرداخت ما به التفاوت از طریق درگاه پرداخت سداد {request.order.OrderCode}";
                    Shared.Models.OrderPay res21 = await CreateOrderPay(request.order,
                        request.wallet,
                        request.payTypeId,
                        walletdescription,
                        walletPrice);
                    payResult.Add(res21);
                    Shared.Models.OrderPay res22 = await CreateOrderPay(request.order,
                        request.wallet,
                        1,
                        Dargahdescription,
                        DargahPrice);
                    payResult.Add(res22);
                    response.IsSuccessed = true;
                }

                break;
            case 6:
                Dargahdescription = $"پرداخت  کل مبلغ سفارش از طریق درگاه پرداخت نوپی {request.order.OrderCode}";
                Shared.Models.OrderPay res6 = await CreateOrderPay(request.order,
                    request.wallet,
                    request.payTypeId,
                    Dargahdescription,
                    request.order.Total);
                payResult.Add(res6);
                response.IsSuccessed = true;
                break;

            default: break;
        }


        response.orderPayResult = payResult;

        return response;
    }

    public async Task<Shared.Models.OrderPay> CreateOrderPay(Shared.Models.Order order,
        Shared.Models.Wallet wallet,
        int payType,
        string description,
        long price)
    {
        Random random = new();
        Shared.Models.OrderPay newItem = new()
        {
            OrderId = order.OrderId,
            PayTypeId = payType,
            PayStatusTypeId = 4,
            Description = description,
            DepositCode = random.Next(10000, 99999).ToString(),
            Price = price
        };
        return await _orderPayRep.AddAsync(newItem);
    }
}