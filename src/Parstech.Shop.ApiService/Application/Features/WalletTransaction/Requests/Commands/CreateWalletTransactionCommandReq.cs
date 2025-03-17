using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.WalletTransaction.Requests.Commands;

public record CreateWalletTransactionCommandReq(WalletTransactionDto TransactionDto, bool admin)
    : IRequest<WalletTransactionResult>;