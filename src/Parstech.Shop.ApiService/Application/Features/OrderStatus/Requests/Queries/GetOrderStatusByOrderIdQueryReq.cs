using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.OrderStatus.Requests.Queries;

public record GetOrderStatusByOrderIdQueryReq(int orderId) : IRequest<List<StatusOfOrderDto>>;