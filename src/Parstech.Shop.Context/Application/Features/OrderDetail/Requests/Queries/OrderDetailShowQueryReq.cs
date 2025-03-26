using MediatR;
using Parstech.Shop.Context.Application.DTOs.Order;

namespace Parstech.Shop.Context.Application.Features.OrderDetail.Requests.Queries;

public record OrderDetailShowQueryReq(int orderId) : IRequest<OrderDetailShowDto>;