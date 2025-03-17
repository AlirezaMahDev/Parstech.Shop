using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.WalletTransaction.Requests.Queries;

public record ReportOfActiveCreditQueryReq(TransactionParameterDto parameter) : IRequest<WalletTransactionPagingDto>;