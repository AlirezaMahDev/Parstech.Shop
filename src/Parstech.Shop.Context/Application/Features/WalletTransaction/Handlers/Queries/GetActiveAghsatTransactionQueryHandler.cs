using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.WalletTransaction;
using Parstech.Shop.Context.Application.Features.WalletTransaction.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.WalletTransaction.Handlers.Queries;

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