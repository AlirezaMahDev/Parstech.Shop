using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Order.Requests.Commands;

public record OrderCreateCommandReq(OrderDto OrderDto) : IRequest<OrderDto>;