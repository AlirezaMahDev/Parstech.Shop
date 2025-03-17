using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Wallet.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.Wallet.Handlers.Queries;

public class GetRemainingOfWalletQueryHandler : IRequestHandler<GetRemainingOfWalletQueryReq, long>
{
    private readonly IWalletRepository _walletRep;

    public GetRemainingOfWalletQueryHandler(IWalletRepository walletRep)
    {
        _walletRep = walletRep;
    }

    public async Task<long> Handle(GetRemainingOfWalletQueryReq request, CancellationToken cancellationToken)
    {
        return await _walletRep.GetRemainingOfWallet(request.userId, request.type);
    }
}