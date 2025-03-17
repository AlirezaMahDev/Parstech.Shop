using MediatR;

using Parstech.Shop.ApiService.Application.DTOs.WalletTransaction;

namespace Parstech.Shop.ApiService.Application.Features.WalletTransaction.Requests.Queries;

public record GetWalletTransactionHistoryQueryReq(
    int WalletId,
    int UserId,
    int Page,
    int PageSize,
    string FromDate,
    string ToDate,
    int TransactionTypeId) : IRequest<WalletTransactionHistoryDto>; 