using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.OrderShipping.Request.Queries;

public record OrderShippingGetByOrderIdQueryReq(int orderId) : IRequest<OrderShippingDto>;