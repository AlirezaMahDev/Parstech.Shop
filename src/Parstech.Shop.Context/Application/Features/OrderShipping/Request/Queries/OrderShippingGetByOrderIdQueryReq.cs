using MediatR;
using Parstech.Shop.Context.Application.DTOs.OrderShipping;

namespace Parstech.Shop.Context.Application.Features.OrderShipping.Request.Queries;

public record OrderShippingGetByOrderIdQueryReq(int orderId):IRequest<OrderShippingDto>;