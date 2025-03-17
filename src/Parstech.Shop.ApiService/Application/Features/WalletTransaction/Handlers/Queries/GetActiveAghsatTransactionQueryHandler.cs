using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.WalletTransaction.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.WalletTransaction.Handlers.Queries;

public class
    GetActiveAghsatTransactionQueryHandler : IRequestHandler<GetActiveAghsatTransactionQueryReq, WalletTransactionDto>
{
    private readonly IWalletTransactionRepository _walletTransactionRep;
    private readonly IMapper _mapper;

    public GetActiveAghsatTransactionQueryHandler(IWalletTransactionRepository walletTransactionRep, IMapper mapper)
    {
        _walletTransactionRep = walletTransactionRep;
        _mapper = mapper;
    }

    public async Task<WalletTransactionDto> Handle(GetActiveAghsatTransactionQueryReq request,
        CancellationToken cancellationToken)
    {
        Shared.Models.WalletTransaction item =
            await _walletTransactionRep.GetActiveAghsat(request.walletId, request.TransactionType);
        return _mapper.Map<WalletTransactionDto>(item);
    }
}