using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Wallet.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.Wallet.Handlers.Queries;

public class WalletBlockOrUnblockQueryHandler : IRequestHandler<WalletBlockOrUnblockQueryReq>
{
    private readonly IWalletRepository _walletRep;

    public WalletBlockOrUnblockQueryHandler(IWalletRepository walletRep)
    {
        _walletRep = walletRep;
    }

    public async Task Handle(WalletBlockOrUnblockQueryReq request, CancellationToken cancellationToken)
    {
        Shared.Models.Wallet? wallet = await _walletRep.GetAsync(request.walletId);
        if (request.block)
        {
            wallet.IsBlock = true;
        }
        else
        {
            wallet.IsBlock = false;
        }

        await _walletRep.UpdateAsync(wallet);
    }
}