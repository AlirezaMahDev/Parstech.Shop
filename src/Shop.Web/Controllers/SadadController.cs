using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.CreditProductStockPrice;
using Shop.Application.DTOs.Order;
using Shop.Application.Features.Order.Requests.Queries;
using Shop.Application.Features.WalletTransaction.Requests.Queries;
using System.Security.Cryptography;
using System.Text;

namespace Shop.Web.Controllers
{

    public class SadadController : Controller
    {



        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRep;
        private readonly ICreditProductStockPriceReopsitory _creidtProductRep;
        private readonly IOrderPayRepository _orderPayRep;
        private readonly IWalletTransactionRepository _walletTransactionRep;

        public SadadController(IMediator mediator,
            IOrderRepository orderRep, IWalletTransactionRepository walletTransactionRep, IOrderPayRepository orderPayRep, ICreditProductStockPriceReopsitory creidtProductRep, IMapper mapper)
        {
            _mediator = mediator;
            _orderRep = orderRep;
            _orderPayRep = orderPayRep;
            _walletTransactionRep = walletTransactionRep;
            _creidtProductRep = creidtProductRep;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CallBack(CallbackRequestPayment result)
        {
            var order = await _orderRep.GetAsync(result.OrderId);
            var transaaction = await _walletTransactionRep.GetLastOfOrder(order.OrderCode);
            string merchantId = "000000140332776";
            string terminalId = "24086411";
            string merchantKey = "uIIgNnYYAUlWuL3nuPysq54HLx4ydJMl";

            var byteData = Encoding.UTF8.GetBytes(result.Token);

            var algorithm = SymmetricAlgorithm.Create("TripleDes");
            algorithm.Mode = CipherMode.ECB;
            algorithm.Padding = PaddingMode.PKCS7;

            var encryptor = algorithm.CreateEncryptor(Convert.FromBase64String(merchantKey), new byte[8]);
            string signData = Convert.ToBase64String(encryptor.TransformFinalBlock(byteData, 0, byteData.Length));

            var data = new
            {
                Token = result.Token,
                SignData = signData
            };

            var verifyRes = CallApi<VerifyResultData>("https://sadad.shaparak.ir/api/v0/Advice/Verify", data).Result;





            if (verifyRes.ResCode == 0)
            {
                var orderPays = await _orderPayRep.GetListByOrderId(result.OrderId);
                if (orderPays.Count == 1)
                {
                    var Result = await _mediator.Send(new CallBackCompleteOrderQueryReq(order.OrderId, transaaction.Id, verifyRes.RetrivalRefNo.ToString()));

                    var redirect = $"https://parstech.co/checkout/payment/Ok";
                    return Redirect(redirect);
                }
                else
                {
                    var secoundOrderPay = orderPays.FirstOrDefault(u => u.PayTypeId != 1);
                    await _mediator.Send(new CompletaOrderWithoutDargahAndStatusAndCallBackQueryReq(order.OrderId, secoundOrderPay.PayTypeId, secoundOrderPay.Price));
                    var Result = await _mediator.Send(new CallBackCompleteOrderQueryReq(order.OrderId, transaaction.Id, verifyRes.RetrivalRefNo.ToString()));


                    var redirect = $"https://parstech.co/checkout/payment/Ok";
                    return Redirect(redirect);
                }
            }
            else
            {


                var redirect = $"https://parstech.co/checkout/payment/NotOk";
                return Redirect(redirect);
            }



        }


        [HttpPost]
        public async Task<IActionResult> CallBackCredit(CallbackRequestPayment result)
        {
            var order = await _orderRep.GetAsync(result.OrderId);
            var credit = await _creidtProductRep.GetAsync(order.CreditId.Value);
            var creditDto=_mapper.Map<CreditProductStockPriceDto>(credit);
            var transaaction = await _walletTransactionRep.GetLastOfOrder(order.OrderCode);
            string merchantId = "000000140332776";
            string terminalId = "24086411";
            string merchantKey = "uIIgNnYYAUlWuL3nuPysq54HLx4ydJMl";

            var byteData = Encoding.UTF8.GetBytes(result.Token);

            var algorithm = SymmetricAlgorithm.Create("TripleDes");
            algorithm.Mode = CipherMode.ECB;
            algorithm.Padding = PaddingMode.PKCS7;

            var encryptor = algorithm.CreateEncryptor(Convert.FromBase64String(merchantKey), new byte[8]);
            string signData = Convert.ToBase64String(encryptor.TransformFinalBlock(byteData, 0, byteData.Length));

            var data = new
            {
                Token = result.Token,
                SignData = signData
            };

            var verifyRes = CallApi<VerifyResultData>("https://sadad.shaparak.ir/api/v0/Advice/Verify", data).Result;





            if (verifyRes.ResCode == 0)
            {
                var orderPays = await _orderPayRep.GetListByOrderId(result.OrderId);
                if (orderPays.Count == 1)
                {
                    var Result = await _mediator.Send(new CallBackCompleteOrderQueryReq(order.OrderId, transaaction.Id, verifyRes.RetrivalRefNo.ToString()));
                    await _mediator.Send(new CreateAghsatForCreditProductQueryReq(order, creditDto));
                    var redirect = $"https://parstech.co/checkout/payment/Ok";
                    return Redirect(redirect);
                }
                else
                {
                    var secoundOrderPay = orderPays.FirstOrDefault(u => u.PayTypeId != 1);
                    await _mediator.Send(new CompletaOrderWithoutDargahAndStatusAndCallBackQueryReq(order.OrderId, secoundOrderPay.PayTypeId, secoundOrderPay.Price));
                    var Result = await _mediator.Send(new CallBackCompleteOrderQueryReq(order.OrderId, transaaction.Id, verifyRes.RetrivalRefNo.ToString()));


                    var redirect = $"https://parstech.co/checkout/payment/Ok";
                    return Redirect(redirect);
                }
            }
            else
            {


                var redirect = $"https://parstech.co/checkout/payment/NotOk";
                return Redirect(redirect);
            }



        }


        [HttpGet]
        [Route("Sadad/BnplCallBack")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> BnplCallBack(CallbackBnplRequestPayment result)
        {
            var order = await _orderRep.GetAsync(result.OrderId);

            if (order == null)
            {
                var redirect = $"https://parstech.co/checkout/payment/NotOk";
                return Redirect(redirect);
            }

            //var transaaction = await _walletTransactionRep.GetLastOfOrder(order.OrderCode);
            string merchantId = "000000140332776";
            string terminalId = "24086411";
            string merchantKey = "uIIgNnYYAUlWuL3nuPysq54HLx4ydJMl";

            var byteData = Encoding.UTF8.GetBytes(result.Token);

            var algorithm = SymmetricAlgorithm.Create("TripleDes");
            algorithm.Mode = CipherMode.ECB;
            algorithm.Padding = PaddingMode.PKCS7;

            var encryptor = algorithm.CreateEncryptor(Convert.FromBase64String(merchantKey), new byte[8]);
            string signData = Convert.ToBase64String(encryptor.TransformFinalBlock(byteData, 0, byteData.Length));

            var data = new
            {
                Token = result.Token,
                SignData = signData
            };

            var verifyRes = CallApi<VerifyResultData>("https://sadad.shaparak.ir/api/v0/BnplAdvice/Verify", data).Result;





            if (verifyRes.ResCode == 0)
            {
                var transaaction = await _walletTransactionRep.GetLastOfOrder(order.OrderCode);
                var orderPays = await _orderPayRep.GetListByOrderId(result.OrderId);
                if (orderPays.Count == 1)
                {
                    var Result = await _mediator.Send(new CallBackCompleteOrderQueryReq(order.OrderId, transaaction.Id, verifyRes.RetrivalRefNo.ToString()));

                    var redirect = $"https://parstech.co/checkout/payment/Ok";
                    return Redirect(redirect);
                }
                else
                {
                    var secoundOrderPay = orderPays.FirstOrDefault(u => u.PayTypeId != 1);
                    await _mediator.Send(new CompletaOrderWithoutDargahAndStatusAndCallBackQueryReq(order.OrderId, secoundOrderPay.PayTypeId, secoundOrderPay.Price));
                    var Result = await _mediator.Send(new CallBackCompleteOrderQueryReq(order.OrderId, transaaction.Id, verifyRes.RetrivalRefNo.ToString()));


                    var redirect = $"https://parstech.co/checkout/payment/Ok";
                    return Redirect(redirect);
                }
            }
            else
            {


                var redirect = $"https://parstech.co/checkout/payment/NotOk";
                return Redirect(redirect);
            }



        }


        public async Task<T> CallApi<T>(string apiUrl, object value) where T : new()
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();

                var json = JsonConvert.SerializeObject(value);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var w = client.PostAsync(apiUrl, content);
                w.Wait();

                HttpResponseMessage response = w.Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync();
                    result.Wait();
                    return JsonConvert.DeserializeObject<T>(result.Result);
                }

                return new T();
            }
        }
    }
}
