using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs.WalletTransaction;
using Parstech.Shop.ApiService.Application.Features.WalletTransaction.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.WalletTransaction.Handlers.Queries;

public class GetWalletTransactionHistoryQueryHandler : IRequestHandler<GetWalletTransactionHistoryQueryReq, WalletTransactionHistoryDto>
{
    private readonly IWalletTransactionRepository _walletTransactionRepository;
    private readonly IMapper _mapper;

    public GetWalletTransactionHistoryQueryHandler(
        IWalletTransactionRepository walletTransactionRepository,
        IMapper mapper)
    {
        _walletTransactionRepository = walletTransactionRepository;
        _mapper = mapper;
    }

    public async Task<WalletTransactionHistoryDto> Handle(
        GetWalletTransactionHistoryQueryReq request,
        CancellationToken cancellationToken)
    {
        var transactions = await _walletTransactionRepository.GetTransactionHistoryAsync(
            request.WalletId,
            request.UserId,
            request.Page,
            request.PageSize,
            request.FromDate,
            request.ToDate,
            request.TransactionTypeId);

        var totalCount = await _walletTransactionRepository.GetTransactionHistoryCountAsync(
            request.WalletId,
            request.UserId,
            request.FromDate,
            request.ToDate,
            request.TransactionTypeId);

        var totalPages = (int)Math.Ceiling((double)totalCount / request.PageSize);

        var result = new WalletTransactionHistoryDto
        {
            TotalCount = totalCount,
            TotalPages = totalPages,
            Transactions = _mapper.Map<List<WalletTransactionDto>>(transactions)
        };

        return result;
    }
} 