using MediatR;

using Parstech.Shop.Context.Application.DTOs.Order;

namespace Parstech.Shop.Context.Application.Features.Order.Requests.Commands;

public record OrderReadCommandReq(int id) : IRequest<OrderDto>;