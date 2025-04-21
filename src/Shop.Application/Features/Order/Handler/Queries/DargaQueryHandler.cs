using Dapper;
using Dto.Response.Payment;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Shop.Application.Dapper.Helper;
using Shop.Application.Dapper.Order.Commands;
using Shop.Application.Dapper.Product.Commands;
using Shop.Application.Dapper.Product.Queries;
using Shop.Application.DTOs.Order;
using Shop.Application.Enum;
using Shop.Application.Features.Order.Requests.Queries;
using Shop.Application.Features.OrderStatus.Requests.Queries;
using Shop.Application.Url;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Order.Handler.Queries
{

    //درگاه سداد
    public class DargaQueryHandler : IRequestHandler<DargaQueryReq, string>
    {
        private readonly IOrderCommand _orderCommand;
        private readonly string _connectionString;
        public DargaQueryHandler(IOrderCommand orderCommand,
            IConfiguration configuration)
        {
            _orderCommand=  orderCommand;
            _connectionString = configuration.GetConnectionString("DatabaseConnection");
        }
        public async Task<string> Handle(DargaQueryReq request, CancellationToken cancellationToken)
        {
            var order = DapperHelper.ExecuteCommand<OrderDto>(_connectionString, conn => conn.Query<OrderDto>(_orderCommand.GetOrderByOrderCode, new { @orderCode = request.orderCode }).FirstOrDefault());

            
            string merchantId = "000000140332776";
            string terminalId = "24086411";
            string merchantKey = "uIIgNnYYAUlWuL3nuPysq54HLx4ydJMl";


            var Amount = request.price*10;
            string signDataBeforeEncode = $"{terminalId};{order.OrderId};{Amount}";


            var byteData = Encoding.UTF8.GetBytes(signDataBeforeEncode);

            var algorithm = SymmetricAlgorithm.Create("TripleDes");
            algorithm.Mode = CipherMode.ECB;
            algorithm.Padding = PaddingMode.PKCS7;

            var encryptor = algorithm.CreateEncryptor(Convert.FromBase64String(merchantKey), new byte[8]);
            string signData = Convert.ToBase64String(encryptor.TransformFinalBlock(byteData, 0, byteData.Length));

            var data = new
            {
                
                MerchantId = merchantId,
                TerminalId = terminalId,
                Amount = Amount,
                OrderId =order.OrderId,
                LocalDateTime = DateTime.Now,
                ReturnUrl = "https://Parstech.co/Sadad/CallBack",
                SignData = signData
            };


            var res = CallApi<RequestPaymentResult>("https://sadad.shaparak.ir/api/v0/Request/PaymentRequest", data).Result;
            if (res.ResCode == 0)
            {
                return $"https://sadad.shaparak.ir/Purchase/Index?token={res.Token}";
            }
            else
            {
                return $"{res.Description}";
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


    //درگاه زرین پال
    public class ZarrinPalQueryHandler : IRequestHandler<ZarrinPalQueryReq, string>
    {
        private readonly IOrderCommand _orderCommand;
        private readonly IMediator _mediator;
        private readonly string _connectionString;
        public ZarrinPalQueryHandler(IOrderCommand orderCommand,
            IConfiguration configuration,
            IMediator mediator)
        {
            _orderCommand = orderCommand;
            _connectionString = configuration.GetConnectionString("DatabaseConnection");
            _mediator = mediator;
        }
        public async Task<string> Handle(ZarrinPalQueryReq request, CancellationToken cancellationToken)
        {
            var order = DapperHelper.ExecuteCommand<OrderDto>(_connectionString, conn => conn.Query<OrderDto>(_orderCommand.GetOrderByOrderCode, new { @orderCode = request.orderCode }).FirstOrDefault());
            //var payment = new ZarinpalSandbox.Payment(Convert.ToInt32(order.Total));
            //var res = payment.PaymentRequest($"پرداخت صورتحساب {order.OrderCode}", $"{BaseUrl.GetUrl()}/Zarrinpal/Callback/{order.OrderId}");
            //if (res.Result.Status == 100)
            //{
            //    await _mediator.Send(new CreateOrderStatusByStatusIdQueryReq(OrderStatusType.OrderAwaitingPayment.ToString(), order.OrderId, order.UserId));

            //    return $"https://sandbox.zarinpal.com/pg/StartPay/{res.Result.Authority}";
            //}
            //else
            //{

            //    await _mediator.Send(new CreateOrderStatusByStatusIdQueryReq(OrderStatusType.CancellationOrderPayment.ToString(), order.OrderId, order.UserId));
            //    return "";
            //}
            ZarinPal.ZarinPal zarinpal = ZarinPal.ZarinPal.Get();
            
            String MerchantID = "85cab9c2-ac10-45a1-8489-1f8650ed6dee";
            String CallbackURL = $"http://parstech.co//Zarrinpal/Callback/{order.OrderId}";
            long Amount = order.Total;
            string Description = $"پرداخت صورتحساب {order.OrderCode}";
            
            ZarinPal.PaymentRequest pr = new ZarinPal.PaymentRequest(MerchantID, Amount, CallbackURL, Description);

            zarinpal.DisableSandboxMode();
            var res = zarinpal.InvokePaymentRequest(pr);
            if (res.Status == 100)
            {
                return res.PaymentURL;
            }
            else
            {

                await _mediator.Send(new CreateOrderStatusByStatusIdQueryReq(OrderStatusType.CancellationOrderPayment.ToString(), order.OrderId, order.UserId));
                return "";
            }
        }
    }



    //درگاه نوپی پیشگامان
    public class NoPayQueryHandler : IRequestHandler<NoPayQueryReq, string>
    {
        private readonly IOrderCommand _orderCommand;
        private readonly string _connectionString;
        public NoPayQueryHandler(IOrderCommand orderCommand,
            IConfiguration configuration)
        {
            _orderCommand = orderCommand;
            _connectionString = configuration.GetConnectionString("DatabaseConnection");
        }
        public async Task<string> Handle(NoPayQueryReq request, CancellationToken cancellationToken)
        {
            var order = DapperHelper.ExecuteCommand<OrderDto>(_connectionString, conn => conn.Query<OrderDto>(_orderCommand.GetOrderByOrderCode, new { @orderCode = request.orderCode }).FirstOrDefault());


            string merchantId = "000000140332776";
            string terminalId = "24086411";
            string merchantKey = "uIIgNnYYAUlWuL3nuPysq54HLx4ydJMl";


            var Amount = request.price * 10;
            string signDataBeforeEncode = $"{terminalId};{order.OrderId};{Amount}";


            var byteData = Encoding.UTF8.GetBytes(signDataBeforeEncode);

            var algorithm = SymmetricAlgorithm.Create("TripleDes");
            algorithm.Mode = CipherMode.ECB;
            algorithm.Padding = PaddingMode.PKCS7;

            var encryptor = algorithm.CreateEncryptor(Convert.FromBase64String(merchantKey), new byte[8]);
            string signData = Convert.ToBase64String(encryptor.TransformFinalBlock(byteData, 0, byteData.Length));

            var data = new
            {

                MerchantId = merchantId,
                TerminalId = terminalId,
                Amount = Amount,
                OrderId = order.OrderId,
                
                ReturnUrl = "https://parstech.co/Sadad/BnplCallBack",
                UserId=order.UserName,
                ApplicationName= "Bnpl",
                PanAuthenticationType=0,
                NationalCode="",
                CardHolderIdentity="",
                SourcePanList="",
                NationalCodeEnc="",
                SignData = signData
            };


            var res = CallApi<NoPayResponseGenKey>("https://bnpl.sadadpsp.ir/Bnpl/GenerateKey", data).Result;
            if (res.ResponseCode == 0)
            {
                return $"https://bnpl.sadadpsp.ir/Home?key={res.BnplKey}";
            }
            else
            {
                return $"{res.Message}";
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






    //درگاه سداد (پرداخت اعتباری)
    public class DargaForCreditProductQueryHandler : IRequestHandler<DargaForCreditProductQueryReq, string>
    {
        private readonly IOrderCommand _orderCommand;
        private readonly string _connectionString;
        public DargaForCreditProductQueryHandler(IOrderCommand orderCommand,
            IConfiguration configuration)
        {
            _orderCommand = orderCommand;
            _connectionString = configuration.GetConnectionString("DatabaseConnection");
        }
        public async Task<string> Handle(DargaForCreditProductQueryReq request, CancellationToken cancellationToken)
        {
            var order = DapperHelper.ExecuteCommand<OrderDto>(_connectionString, conn => conn.Query<OrderDto>(_orderCommand.GetOrderByOrderCode, new { @orderCode = request.orderCode }).FirstOrDefault());


            string merchantId = "000000140332776";
            string terminalId = "24086411";
            string merchantKey = "uIIgNnYYAUlWuL3nuPysq54HLx4ydJMl";


            var Amount = request.price * 10;
            string signDataBeforeEncode = $"{terminalId};{order.OrderId};{Amount}";


            var byteData = Encoding.UTF8.GetBytes(signDataBeforeEncode);

            var algorithm = SymmetricAlgorithm.Create("TripleDes");
            algorithm.Mode = CipherMode.ECB;
            algorithm.Padding = PaddingMode.PKCS7;

            var encryptor = algorithm.CreateEncryptor(Convert.FromBase64String(merchantKey), new byte[8]);
            string signData = Convert.ToBase64String(encryptor.TransformFinalBlock(byteData, 0, byteData.Length));

            var data = new
            {

                MerchantId = merchantId,
                TerminalId = terminalId,
                Amount = Amount,
                OrderId = order.OrderId,
                LocalDateTime = DateTime.Now,
                ReturnUrl = $"https://Parstech.co/Sadad/CallBackCredit",
                //ReturnUrl = $"https://localhost:7040/Sadad/CallBackCredit",
                SignData = signData
            };


            var res = CallApi<RequestPaymentResult>("https://sadad.shaparak.ir/api/v0/Request/PaymentRequest", data).Result;
            if (res.ResCode == 0)
            {
                return $"https://sadad.shaparak.ir/Purchase/Index?token={res.Token}";
            }
            else
            {
                return $"{res.Description}";
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
