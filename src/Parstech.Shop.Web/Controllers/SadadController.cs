using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Parstech.Shop.Web.Services.GrpcClients;
using Shop.Application.DTOs.Order;
using System.Security.Cryptography;
using System.Text;

namespace Shop.Web.Controllers
{
    public class SadadController : Controller
    {
        private readonly OrderCheckoutGrpcClient _orderCheckoutClient;
        private readonly WalletGrpcClient _walletClient;
        
        public SadadController(
            OrderCheckoutGrpcClient orderCheckoutClient,
            WalletGrpcClient walletClient)
        {
            _orderCheckoutClient = orderCheckoutClient;
            _walletClient = walletClient;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CallBack(CallbackRequestPayment result)
        {
            // Get order details using gRPC client
            var order = await _orderCheckoutClient.GetOrderDetailsAsync(result.OrderId);
            
            // Get transaction details using gRPC client
            var transaction = await _walletClient.GetActiveTransactionAsync(order.OrderId, "Order");
            
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
                // Get order payment details
                var orderPayments = await _orderCheckoutClient.GetOrderPaymentsAsync(result.OrderId);
                
                if (orderPayments.Count == 1)
                {
                    // Complete the order using gRPC client
                    await _orderCheckoutClient.CompleteOrderAsync(
                        order.OrderId,
                        order.ShippingId,
                        2, // Payment type ID for Sadad
                        transaction.TransactionId,
                        verifyRes.RetrivalRefNo.ToString());
                    
                    var redirect = $"https://parstech.co/checkout/payment/Ok";
                    return Redirect(redirect);
                }
                else
                {
                    var secondOrderPay = orderPayments.FirstOrDefault(p => p.PayTypeId != 1);
                    
                    // Complete the order with multiple payment types
                    await _orderCheckoutClient.CompleteOrderWithMultiplePaymentsAsync(
                        order.OrderId,
                        secondOrderPay.PayTypeId,
                        secondOrderPay.Amount);
                    
                    // Complete the main payment
                    await _orderCheckoutClient.CompleteOrderAsync(
                        order.OrderId,
                        order.ShippingId,
                        2, // Payment type ID for Sadad
                        transaction.TransactionId,
                        verifyRes.RetrivalRefNo.ToString());
                    
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
            // Get order details using gRPC client
            var order = await _orderCheckoutClient.GetOrderDetailsAsync(result.OrderId);
            
            if (order == null || order.OrderId == 0)
            {
                var redirect = $"https://parstech.co/checkout/payment/NotOk";
                return Redirect(redirect);
            }

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
                // Get transaction details using gRPC client
                var transaction = await _walletClient.GetActiveTransactionAsync(order.OrderId, "Order");
                
                // Get order payment details
                var orderPayments = await _orderCheckoutClient.GetOrderPaymentsAsync(result.OrderId);
                
                if (orderPayments.Count == 1)
                {
                    // Complete the order using gRPC client
                    await _orderCheckoutClient.CompleteOrderAsync(
                        order.OrderId,
                        order.ShippingId,
                        2, // Payment type ID for Sadad
                        transaction.TransactionId,
                        verifyRes.RetrivalRefNo.ToString());
                    
                    var redirect = $"https://parstech.co/checkout/payment/Ok";
                    return Redirect(redirect);
                }
                else
                {
                    var secondOrderPay = orderPayments.FirstOrDefault(p => p.PayTypeId != 1);
                    
                    // Complete the order with multiple payment types
                    await _orderCheckoutClient.CompleteOrderWithMultiplePaymentsAsync(
                        order.OrderId,
                        secondOrderPay.PayTypeId,
                        secondOrderPay.Amount);
                    
                    // Complete the main payment
                    await _orderCheckoutClient.CompleteOrderAsync(
                        order.OrderId,
                        order.ShippingId,
                        2, // Payment type ID for Sadad
                        transaction.TransactionId,
                        verifyRes.RetrivalRefNo.ToString());
                    
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
