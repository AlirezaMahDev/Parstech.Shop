using MediatR;
using Parstech.Shop.Context.Application.DTOs.Paging;
using Parstech.Shop.Context.Application.DTOs.WalletTransaction;

namespace Parstech.Shop.Context.Application.Features.WalletTransaction.Requests.Queries;

public record WalletTransactionsPagingQueryReq(WalletTransactionParameterDto Parameter) : IRequest<PagingDto>;