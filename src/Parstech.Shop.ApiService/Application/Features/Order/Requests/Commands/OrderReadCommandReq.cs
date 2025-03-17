using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Order.Requests.Commands;

public record OrderReadCommandReq(int id) : IRequest<OrderDto>;