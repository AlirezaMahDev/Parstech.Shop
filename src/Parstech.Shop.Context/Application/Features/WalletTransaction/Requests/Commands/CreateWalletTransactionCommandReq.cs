using MediatR;
using Parstech.Shop.Context.Application.DTOs.WalletTransaction;

namespace Parstech.Shop.Context.Application.Features.WalletTransaction.Requests.Commands;

public record CreateWalletTransactionCommandReq(WalletTransactionDto TransactionDto,bool admin): IRequest<WalletTransactionResult>;