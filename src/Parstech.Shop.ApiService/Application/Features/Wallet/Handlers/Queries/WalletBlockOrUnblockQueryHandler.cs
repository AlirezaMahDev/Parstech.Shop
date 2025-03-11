using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Features.Wallet.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Wallet.Handlers.Queries
{
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
}
