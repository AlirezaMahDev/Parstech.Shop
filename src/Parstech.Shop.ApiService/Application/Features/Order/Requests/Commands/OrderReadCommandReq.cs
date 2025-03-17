using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Order.Requests.Commands;

public record OrderReadCommandReq(int id) : IRequest<OrderDto>;