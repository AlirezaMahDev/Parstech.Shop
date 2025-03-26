using MediatR;
using Parstech.Shop.Context.Application.DTOs.OrderDetail;
using Parstech.Shop.Context.Application.DTOs.Order;

namespace Parstech.Shop.Context.Application.Features.OrderDetail.Requests.Queries;

public record RefreshOrderDetailQueryReq(OrderDetailDto OrderDetailDto) : IRequest<OrderResponse>;