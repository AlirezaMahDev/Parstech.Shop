using MediatR;

using Parstech.Shop.Context.Application.DTOs.OrderDetail;

namespace Parstech.Shop.Context.Application.Features.OrderDetail.Requests.Queries;

public record OrderDetailsOfOrderQueryReq(int orderId) : IRequest<List<OrderDetailDto>>;