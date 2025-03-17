using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.OrderDetail.Requests.Queries;

public record OrderDetailsOfOrderQueryReq(int orderId) : IRequest<List<OrderDetailDto>>;