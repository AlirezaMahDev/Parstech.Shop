using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.WalletTransaction.Requests.Queries;

public record GetActiveAghsatTransactionQueryReq(int walletId, string TransactionType) : IRequest<WalletTransactionDto>;