using MediatR;
using Parstech.Shop.Context.Application.DTOs.Paging;

namespace Parstech.Shop.Context.Application.Features.PropertyCategury.Requests.Queries;

public record PropertyCateguryPagingQueryReq(ParameterDto Parameter) : IRequest<PagingDto>;