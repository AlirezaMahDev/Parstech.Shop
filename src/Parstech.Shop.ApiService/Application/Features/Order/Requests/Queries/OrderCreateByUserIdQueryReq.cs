using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Order.Requests.Queries;

public record OrderCreateByUserIdQueryReq(int userId) : IRequest<OrderDto>;