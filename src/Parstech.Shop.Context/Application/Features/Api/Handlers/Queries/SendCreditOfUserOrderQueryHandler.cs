using MediatR;

using Newtonsoft.Json;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Api;
using Parstech.Shop.Context.Application.Features.Api.Requests.Queries;

using RestSharp;

namespace Parstech.Shop.Context.Application.Features.Api.Handlers.Queries;

public class SendCreditOfUserOrderQueryHandler : IRequestHandler<SendCreditOfUserOrderQueryReq, bool>
{
    private readonly RestClient client;
    private readonly IUserRepository _userRep;
    private readonly IUserBillingRepository _userBillingRep;
    private readonly IWalletRepository _walletRep;
    private readonly IOrderRepository _orderRep;
    private readonly RestRequest Request;
    private readonly IMediator _mediator;

    public SendCreditOfUserOrderQueryHandler(IUserRepository userRep,
        IWalletRepository walletRep,
        IOrderRepository orderRep,
        IUserBillingRepository userBillingRep,
        IMediator mediator)
    {
        client = new("http://185.208.78.102:900");
        _userRep = userRep;
        _walletRep = walletRep;
        _orderRep = orderRep;
        _userBillingRep = userBillingRep;
        _mediator = mediator;
        Request = new("/api/public/buy", Method.Post);
    }
    public async Task<bool> Handle(SendCreditOfUserOrderQueryReq request, CancellationToken cancellationToken)
    {
        var order = await _orderRep.GetAsync(request.orderId);

        var userBilling = await _userBillingRep.GetUserBillingByUserId(order.UserId);



        var phone = 0;
        HttpClient clients = new();
        ApiUserInfoRoot Result = new();
        var path = $"http://185.208.78.102:900/api/public/userinfo?nationalcode={userBilling.NationalCode}";
        try
        {
            HttpResponseMessage response = await clients.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                Result = JsonConvert.DeserializeObject<ApiUserInfoRoot>(res);
                phone = Result.result.numbers[0].id;


                ApiBuyClass Resourse = new();
                var rial = order.Total * 10;
                Resourse.creditTransactionValue = Convert.ToInt32(rial);
                Resourse.nationalCode = userBilling.NationalCode;
                Resourse.phoneNumberId = phone;
                try
                {
                    string JsonData = JsonConvert.SerializeObject(Resourse);
                    Request.AddJsonBody(JsonData);

                    var finalResult = client.Post(Request);
                    await _mediator.Send(new GetCreditOfNationalCodeQueryReq(userBilling.UserId, userBilling.NationalCode));
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }

            }
        }
        catch (Exception e)
        {
            return false;
        }

        return false;
    }
}