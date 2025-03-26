using MediatR;
using Parstech.Shop.Context.Application.DTOs.WalletTransaction;

namespace Parstech.Shop.Context.Application.Features.WalletTransaction.Requests.Queries;

public record GetTransactionByParentIdQueryReq(int id):IRequest<List<WalletTransactionDto>>;