using MediatR;
using Parstech.Shop.Context.Application.DTOs.Paging;

namespace Parstech.Shop.Context.Application.Features.Categury.Requests.Queries;

public record CateguryPagingQueryReq(ParameterDto Parameter) : IRequest<PagingDto>;