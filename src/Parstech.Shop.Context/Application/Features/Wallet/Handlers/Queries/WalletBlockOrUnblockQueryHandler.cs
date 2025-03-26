using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Features.Wallet.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.Wallet.Handlers.Queries;

public class WalletBlockOrUnblockQueryHandler : IRequestHandler<WalletBlockOrUnblockQueryReq>
{
    private readonly IWalletRepository _walletRep;
    public WalletBlockOrUnblockQueryHandler(IWalletRepository walletRep)
    {
        _walletRep = walletRep;
    }
    public async Task Handle(WalletBlockOrUnblockQueryReq request, CancellationToken cancellationToken)
    {
        var wallet = await _walletRep.GetAsync(request.walletId);
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