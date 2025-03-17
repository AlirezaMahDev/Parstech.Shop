using Dapper;

using MediatR;

using Newtonsoft.Json;

using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.Dapper.Order.Commands;
using Parstech.Shop.ApiService.Application.Dargah.ZarrinPal.Models;
using Parstech.Shop.ApiService.Application.Enum;
using Parstech.Shop.ApiService.Application.Features.Order.Requests.Queries;

using System.Security.Cryptography;
using System.Text;

using Parstech.Shop.ApiService.Application.Features.OrderStatus.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Order.Handler.Queries;

//درگاه سداد
public class DargaQueryHandler : IRequestHandler<DargaQueryReq, string>
{
    private readonly IOrderCommand _orderCommand;
    private readonly string _connectionString;

    public DargaQueryHandler(IOrderCommand orderCommand,
        IConfiguration configuration)
    {
        _orderCommand = orderCommand;
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }

    public async Task<string> Handle(DargaQueryReq request, CancellationToken cancellationToken)
    {
        OrderDto order = DapperHelper.ExecuteCommand<OrderDto>(_connectionString,
            conn => conn.Query<OrderDto>(_orderCommand.GetOrderByOrderCode, new { request.orderCode })
                .FirstOrDefault());


        string merchantId = "000000140332776";
        string terminalId = "24086411";
        string merchantKey = "uIIgNnYYAUlWuL3nuPysq54HLx4ydJMl";


        long amount = request.price * 10;
        string signDataBeforeEncode = $"{terminalId};{order.OrderId};{amount}";


        byte[] byteData = Encoding.UTF8.GetBytes(signDataBeforeEncode);

        SymmetricAlgorithm? algorithm = SymmetricAlgorithm.Create("TripleDes");
        algorithm.Mode = CipherMode.ECB;
        algorithm.Padding = PaddingMode.PKCS7;

        ICryptoTransform encryptor = algorithm.CreateEncryptor(Convert.FromBase64String(merchantKey), new byte[8]);
        string signData = Convert.ToBase64String(encryptor.TransformFinalBlock(byteData, 0, byteData.Length));

        var data = new
        {
            MerchantId = merchantId,
            TerminalId = terminalId,
            Amount = amount,
            order.OrderId,
            LocalDateTime = DateTime.Now,
            ReturnUrl = "https://Parstech.co/Sadad/CallBack",
            SignData = signData
        };


        RequestPaymentResult res =
            CallApi<RequestPaymentResult>("https://sadad.shaparak.ir/api/v0/Request/PaymentRequest", data).Result;
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
        using (HttpClient client = new())
        {
            client.BaseAddress = new(apiUrl);
            client.DefaultRequestHeaders.Accept.Clear();

            string? json = JsonConvert.SerializeObject(value);
            StringContent content = new(json, Encoding.UTF8, "application/json");

            Task<HttpResponseMessage> w = client.PostAsync(apiUrl, content);
            w.Wait();

            HttpResponseMessage response = w.Result;
            if (response.IsSuccessStatusCode)
            {
                Task<string> result = response.Content.ReadAsStringAsync();
                result.Wait();
                return JsonConvert.DeserializeObject<T>(result.Result);
            }

            return new();
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
        OrderDto order = DapperHelper.ExecuteCommand<OrderDto>(_connectionString,
            conn => conn.Query<OrderDto>(_orderCommand.GetOrderByOrderCode, new { request.orderCode })
                .FirstOrDefault());
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
        Dargah.ZarrinPal.ZarinPal zarinpal = Dargah.ZarrinPal.ZarinPal.Get();

        string merchantId = "85cab9c2-ac10-45a1-8489-1f8650ed6dee";
        string callbackUrl = $"http://parstech.co//Zarrinpal/Callback/{order.OrderId}";
        long amount = order.Total;
        string description = $"پرداخت صورتحساب {order.OrderCode}";

        PaymentRequest pr = new(merchantId, amount, callbackUrl, description);

        zarinpal.DisableSandboxMode();
        PaymentResponse res = zarinpal.InvokePaymentRequest(pr);
        if (res.Status == 100)
        {
            return res.PaymentURL;
        }
        else
        {
            await _mediator.Send(new CreateOrderStatusByStatusIdQueryReq(
                OrderStatusType.CancellationOrderPayment.ToString(),
                order.OrderId,
                order.UserId));
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
        OrderDto order = DapperHelper.ExecuteCommand<OrderDto>(_connectionString,
            conn => conn.Query<OrderDto>(_orderCommand.GetOrderByOrderCode, new { request.orderCode })
                .FirstOrDefault());


        string merchantId = "000000140332776";
        string terminalId = "24086411";
        string merchantKey = "uIIgNnYYAUlWuL3nuPysq54HLx4ydJMl";


        long amount = request.price * 10;
        string signDataBeforeEncode = $"{terminalId};{order.OrderId};{amount}";


        byte[] byteData = Encoding.UTF8.GetBytes(signDataBeforeEncode);

        SymmetricAlgorithm? algorithm = SymmetricAlgorithm.Create("TripleDes");
        algorithm.Mode = CipherMode.ECB;
        algorithm.Padding = PaddingMode.PKCS7;

        ICryptoTransform encryptor = algorithm.CreateEncryptor(Convert.FromBase64String(merchantKey), new byte[8]);
        string signData = Convert.ToBase64String(encryptor.TransformFinalBlock(byteData, 0, byteData.Length));

        var data = new
        {
            MerchantId = merchantId,
            TerminalId = terminalId,
            Amount = amount,
            order.OrderId,
            ReturnUrl = "https://parstech.co/Sadad/BnplCallBack",
            UserId = order.UserName,
            ApplicationName = "Bnpl",
            PanAuthenticationType = 0,
            NationalCode = "",
            CardHolderIdentity = "",
            SourcePanList = "",
            NationalCodeEnc = "",
            SignData = signData
        };


        NoPayResponseGenKey res = CallApi<NoPayResponseGenKey>("https://bnpl.sadadpsp.ir/Bnpl/GenerateKey", data)
            .Result;
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
        using (HttpClient client = new())
        {
            client.BaseAddress = new(apiUrl);
            client.DefaultRequestHeaders.Accept.Clear();

            string? json = JsonConvert.SerializeObject(value);
            StringContent content = new(json, Encoding.UTF8, "application/json");

            Task<HttpResponseMessage> w = client.PostAsync(apiUrl, content);
            w.Wait();

            HttpResponseMessage response = w.Result;
            if (response.IsSuccessStatusCode)
            {
                Task<string> result = response.Content.ReadAsStringAsync();
                result.Wait();
                return JsonConvert.DeserializeObject<T>(result.Result);
            }

            return new();
        }
    }
}