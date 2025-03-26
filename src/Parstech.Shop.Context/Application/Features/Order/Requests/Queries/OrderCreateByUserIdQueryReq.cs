using MediatR;

using Parstech.Shop.Context.Application.DTOs.Order;

namespace Parstech.Shop.Context.Application.Features.Order.Requests.Queries;

public record OrderCreateByUserIdQueryReq(int userId):IRequest<OrderDto>;