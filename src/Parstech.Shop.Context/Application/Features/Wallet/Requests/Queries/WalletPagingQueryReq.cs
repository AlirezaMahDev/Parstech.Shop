using MediatR;

using Parstech.Shop.Context.Application.DTOs.Paging;

namespace Parstech.Shop.Context.Application.Features.Wallet.Requests.Queries;

public record WalletPagingQueryReq(ParameterDto Parameter) : IRequest<PagingDto>;