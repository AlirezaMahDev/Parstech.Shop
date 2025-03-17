using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.OrderShipping.Request.Queries;

public record OrderShippingGetByOrderIdQueryReq(int orderId) : IRequest<OrderShippingDto>;