using MediatR;
using Newtonsoft.Json;
using RestSharp;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Api;
using Shop.Application.DTOs.UserBilling;
using Shop.Application.Features.Api.Requests.Queries;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Shop.Application.Features.Api.Handlers.Queries
{
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
            client = new RestClient("http://185.208.78.102:900");
            _userRep = userRep;
            _walletRep = walletRep;
            _orderRep = orderRep;
            _userBillingRep = userBillingRep;
            _mediator = mediator;
            Request = new RestRequest("/api/public/buy", Method.Post);
        }
        public async Task<bool> Handle(SendCreditOfUserOrderQueryReq request, CancellationToken cancellationToken)
        {
            var order = await _orderRep.GetAsync(request.orderId);

            var userBilling = await _userBillingRep.GetUserBillingByUserId(order.UserId);



            var phone = 0;
            HttpClient clients = new HttpClient();
            ApiUserInfoRoot Result = new ApiUserInfoRoot();
            var path = $"http://185.208.78.102:900/api/public/userinfo?nationalcode={userBilling.NationalCode}";
            try
            {
                HttpResponseMessage response = await clients.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStringAsync();
                    Result = JsonConvert.DeserializeObject<ApiUserInfoRoot>(res);
                    phone = Result.result.numbers[0].id;


                    ApiBuyClass Resourse = new ApiBuyClass();
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
}
