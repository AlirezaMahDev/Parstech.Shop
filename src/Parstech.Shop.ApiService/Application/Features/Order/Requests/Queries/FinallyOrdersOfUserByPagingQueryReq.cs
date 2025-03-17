using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Order.Requests.Queries;

public record FinallyOrdersOfUserByPagingQueryReq(int userId, ParameterDto Parameter) : IRequest<PagingDto>;