using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.WalletTransaction;
using Shop.Application.Features.WalletTransaction.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.WalletTransaction.Handlers.Queries
{
    public class GetActiveAghsatTransactionQueryHandler : IRequestHandler<GetActiveAghsatTransactionQueryReq, WalletTransactionDto>
    {
        private readonly IWalletTransactionRepository _walletTransactionRep;
        private readonly IMapper _mapper;
        public GetActiveAghsatTransactionQueryHandler(IWalletTransactionRepository walletTransactionRep,IMapper mapper)
        {
            _walletTransactionRep = walletTransactionRep;   
            _mapper = mapper;
        }
        public async Task<WalletTransactionDto> Handle(GetActiveAghsatTransactionQueryReq request, CancellationToken cancellationToken)
        {
            var item = await _walletTransactionRep.GetActiveAghsat(request.walletId, request.TransactionType);
            return _mapper.Map<WalletTransactionDto>(item);
        }
    }
}
