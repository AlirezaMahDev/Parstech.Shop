using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.OrderDetail.Requests.Queries;

public record OrderDetailShowQueryReq(int orderId) : IRequest<OrderDetailShowDto>;