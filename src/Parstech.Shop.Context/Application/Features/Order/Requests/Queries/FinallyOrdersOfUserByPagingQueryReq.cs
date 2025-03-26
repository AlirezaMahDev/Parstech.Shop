using MediatR;

using Parstech.Shop.Context.Application.DTOs.Paging;

namespace Parstech.Shop.Context.Application.Features.Order.Requests.Queries;

public record FinallyOrdersOfUserByPagingQueryReq(int userId, ParameterDto Parameter):IRequest<PagingDto>;