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
}
