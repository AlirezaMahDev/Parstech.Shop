using MediatR;

using Newtonsoft.Json;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Api.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Api.Handlers.Queries;

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
        int tomanCredit = 0;
        Root Result = new();

        Shared.Models.User? user = await _userRep.GetAsync(request.userId);
        Shared.Models.Wallet wallet = await _walletRep.GetWalletByUserId(request.userId);
        HttpClient clients = new();

        string path = $"http://185.208.78.102:900/api/public/usercredits?nationalcode={request.nationalCode}";
        try
        {
            HttpResponseMessage response = await clients.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                string res = await response.Content.ReadAsStringAsync();
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