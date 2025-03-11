using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Wallet;
using Shop.Application.Features.Wallet.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Wallet.Handlers.Queries
{
    public class GetWalletByUserIdQueryHandler : IRequestHandler<GetWalletByUserIdQueryReq, WalletDto>
    {
        private readonly IWalletRepository _walletRep;
        private readonly IMapper _mapper;
        public GetWalletByUserIdQueryHandler(IWalletRepository walletRep,
            IMapper mapper)
        {
            _walletRep = walletRep;
            _mapper = mapper;
        }
        public async Task<WalletDto> Handle(GetWalletByUserIdQueryReq request, CancellationToken cancellationToken)
        {
            var wallet = await _walletRep.GetWalletByUserId(request.userId);
            return _mapper.Map<WalletDto>(wallet);
                
        }
    }
}
