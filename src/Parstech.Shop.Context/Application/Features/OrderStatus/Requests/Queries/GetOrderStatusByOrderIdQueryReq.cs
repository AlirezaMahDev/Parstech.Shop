using MediatR;
using Parstech.Shop.Context.Application.DTOs.OrderStatus;

namespace Parstech.Shop.Context.Application.Features.OrderStatus.Requests.Queries;

public record GetOrderStatusByOrderIdQueryReq(int orderId) : IRequest<List<StatusOfOrderDto>>;