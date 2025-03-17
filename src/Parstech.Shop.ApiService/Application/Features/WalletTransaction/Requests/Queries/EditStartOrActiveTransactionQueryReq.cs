using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.WalletTransaction.Requests.Queries;

public record EditStartOrActiveTransactionQueryReq(int transactionId, string startOrActive, bool? start, bool? active)
    : IRequest;