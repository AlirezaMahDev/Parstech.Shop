using Dto.Response.Payment;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.OrderPay;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.OrderPay.Request.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.OrderPay.Handler.Queries
{
    public class ChoisePayTypeForCreateOrderPayQueryHandler : IRequestHandler<ChoisePayTypeForCreateOrderPayQueryReq, ResponseOrderPayDto>
    {
        private readonly IOrderPayRepository _orderPayRep;
        public ChoisePayTypeForCreateOrderPayQueryHandler(IOrderPayRepository orderPayRep)
        {
            _orderPayRep = orderPayRep;
        }
        public async Task<ResponseOrderPayDto> Handle(ChoisePayTypeForCreateOrderPayQueryReq request, CancellationToken cancellationToken)
        {
            ResponseOrderPayDto response = new ResponseOrderPayDto();
            List<Domain.Models.OrderPay> payResult = new List<Domain.Models.OrderPay>();
            long walletPrice = 0;
            string walletdescription = "";
            long DargahPrice = 0;
            string Dargahdescription = "";

            if (await _orderPayRep.HasOrderPay(request.order.OrderId))
            {

                var items = await _orderPayRep.GetListByOrderId(request.order.OrderId);
                foreach (var item in items)
                {
                    await _orderPayRep.DeleteAsync(item);
                }
            }
            switch (request.payTypeId)
            {
                case 1:
                    Dargahdescription = $"پرداخت  کل مبلغ سفارش از طریق درگاه پرداخت سداد {request.order.OrderCode}";
                    var res1 = await CreateOrderPay(request.order, request.wallet, request.payTypeId, Dargahdescription, request.order.Total);
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
                        var res2 = await CreateOrderPay(request.order, request.wallet, request.payTypeId, walletdescription, request.order.Total);
                        payResult.Add(res2);
                        response.IsSuccessed = true;
                    }
                    else
                    {
                        walletPrice = request.wallet.Amount;
                        walletdescription = $"پرداخت بخشی از مبلغ سفارش از کیف پول سفارش {request.order.OrderCode}";
                        DargahPrice = request.order.Total - request.wallet.Amount;
                        Dargahdescription = $"پرداخت ما به التفاوت از طریق درگاه پرداخت سداد {request.order.OrderCode}";
                        var res21 = await CreateOrderPay(request.order, request.wallet, request.payTypeId, walletdescription, walletPrice);
                        payResult.Add(res21);
                        var res22 = await CreateOrderPay(request.order, request.wallet, 1, Dargahdescription, DargahPrice);
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
                        var res2 = await CreateOrderPay(request.order, request.wallet, request.payTypeId, walletdescription, request.order.Total);
                        payResult.Add(res2);
                        response.IsSuccessed = true;
                    }
                    else
                    {
                        walletPrice = request.wallet.Fecilities;
                        walletdescription = $"پرداخت بخشی از مبلغ سفارش از تسهیلات سفارش {request.order.OrderCode}";
                        DargahPrice = request.order.Total - request.wallet.Fecilities;
                        Dargahdescription = $"پرداخت ما به التفاوت از طریق درگاه پرداخت سداد {request.order.OrderCode}";
                        var res21 = await CreateOrderPay(request.order, request.wallet, request.payTypeId, walletdescription, walletPrice);
                        payResult.Add(res21);
                        var res22 = await CreateOrderPay(request.order, request.wallet, 1, Dargahdescription, DargahPrice);
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
                        var res2 = await CreateOrderPay(request.order, request.wallet, request.payTypeId, walletdescription, request.order.Total);
                        payResult.Add(res2);
                        response.IsSuccessed = true;
                    }
                    else
                    {
                        walletPrice = request.wallet.OrgCredit;
                        walletdescription = $"پرداخت بخشی از مبلغ سفارش از اعتبار سازمانی سفارش {request.order.OrderCode}";
                        DargahPrice = request.order.Total - request.wallet.OrgCredit;
                        Dargahdescription = $"پرداخت ما به التفاوت از طریق درگاه پرداخت سداد {request.order.OrderCode}";
                        var res21 = await CreateOrderPay(request.order, request.wallet, request.payTypeId, walletdescription, walletPrice);
                        payResult.Add(res21);
                        var res22 = await CreateOrderPay(request.order, request.wallet, 1, Dargahdescription, DargahPrice);
                        payResult.Add(res22);
                        response.IsSuccessed = true;
                    }
                    break;
                case 6:
                    Dargahdescription = $"پرداخت  کل مبلغ سفارش از طریق درگاه پرداخت نوپی {request.order.OrderCode}";
                    var res6 = await CreateOrderPay(request.order, request.wallet, request.payTypeId, Dargahdescription, request.order.Total);
                    payResult.Add(res6);
                    response.IsSuccessed = true;
                    break;

                default: break;
            }


            response.orderPayResult = payResult;

            return response;
        }
        public async Task<Domain.Models.OrderPay> CreateOrderPay(Domain.Models.Order order, Domain.Models.Wallet wallet, int payType, string description, long price)
        {
            Random random = new Random();
            Domain.Models.OrderPay newItem = new Domain.Models.OrderPay()
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


    public class ChoisePayTypeForCreateOrderPayForCreditProductQueryHandler : IRequestHandler<ChoisePayTypeForCreateOrderPayForCreditProductQueryReq, ResponseOrderPayDto>
    {
        private readonly IOrderPayRepository _orderPayRep;
        public ChoisePayTypeForCreateOrderPayForCreditProductQueryHandler(IOrderPayRepository orderPayRep)
        {
            _orderPayRep = orderPayRep;
        }
        public async Task<ResponseOrderPayDto> Handle(ChoisePayTypeForCreateOrderPayForCreditProductQueryReq request, CancellationToken cancellationToken)
        {
            ResponseOrderPayDto response = new ResponseOrderPayDto();
            List<Domain.Models.OrderPay> payResult = new List<Domain.Models.OrderPay>();
            long walletPrice = 0;
            string walletdescription = "";
            long DargahPrice = 0;
            string Dargahdescription = "";

            if (await _orderPayRep.HasOrderPay(request.order.OrderId))
            {

                var items = await _orderPayRep.GetListByOrderId(request.order.OrderId);
                foreach (var item in items)
                {
                    await _orderPayRep.DeleteAsync(item);
                }
            }


            long total = request.order.Total - request.credit.Total + request.credit.PrePay;
            switch (request.payTypeId)
            {
               
                case 7:
                    Dargahdescription = $"پرداخت اعتباری با پیش پرداخت {request.order.OrderCode}";
                    var res7 = await CreateOrderPay(request.order, request.wallet, request.payTypeId, Dargahdescription, total);
                    payResult.Add(res7);
                    response.IsSuccessed = true;
                    break;
                
                case 8:
                    Dargahdescription = $"پرداخت اعتباری بدون پیش پرداخت {request.order.OrderCode}";
                    var res8 = await CreateOrderPay(request.order, request.wallet, request.payTypeId, Dargahdescription, total);
                    payResult.Add(res8);
                    response.IsSuccessed = true;
                    break;

                default: break;
            }


            response.orderPayResult = payResult;

            return response;
        }
        public async Task<Domain.Models.OrderPay> CreateOrderPay(Domain.Models.Order order, Domain.Models.Wallet wallet, int payType, string description, long price)
        {
            Random random = new Random();
            Domain.Models.OrderPay newItem = new Domain.Models.OrderPay()
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
}
