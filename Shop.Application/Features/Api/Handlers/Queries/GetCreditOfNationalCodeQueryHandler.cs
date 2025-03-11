using Dto.Response.Payment;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Api;
using Shop.Application.Features.Api.Requests.Queries;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Shop.Application.Features.Api.Handlers.Queries
{
    public class GetCreditOfNationalCodeQueryHandler : IRequestHandler<GetCreditOfNationalCodeQueryReq, int>
    {
        //private readonly RestClient client;

        private readonly IUserRepository _userRep;
        private readonly IWalletRepository _walletRep;

        public GetCreditOfNationalCodeQueryHandler(IUserRepository userRep, IWalletRepository walletRep)
        {
            //client = new RestClient("http://5.144.134.116:900");
            _userRep = userRep;
            _walletRep = walletRep;
        }
        public async Task<int> Handle(GetCreditOfNationalCodeQueryReq request, CancellationToken cancellationToken)
        {
            var tomanCredit = 0;
            Root Result = new Root();

            var user = await _userRep.GetAsync(request.userId);
            var wallet = await _walletRep.GetWalletByUserId(request.userId);
            HttpClient clients = new HttpClient();

            var path = $"http://185.208.78.102:900/api/public/usercredits?nationalcode={request.nationalCode}";
            try
            {
                HttpResponseMessage response = await clients.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStringAsync();
                    Result = JsonConvert.DeserializeObject<Root>(res);
                    
                    if (Result.result.cashCredit > 10)
                    {
                        tomanCredit = Result.result.cashCredit / 10;
                    }

                    wallet.OrgCredit = tomanCredit;
                    await _walletRep.UpdateAsync(wallet);
                }
            }
            catch (Exception e)
            {
                tomanCredit = 0;

            }
            return tomanCredit;
        }
    }
}
