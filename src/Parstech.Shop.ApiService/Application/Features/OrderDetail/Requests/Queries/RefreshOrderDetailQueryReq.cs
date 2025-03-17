using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.OrderDetail.Requests.Queries;

public record RefreshOrderDetailQueryReq(OrderDetailDto OrderDetailDto) : IRequest<OrderResponse>;