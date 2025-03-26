using MediatR;

using Parstech.Shop.Context.Application.DTOs.Order;

namespace Parstech.Shop.Context.Application.Features.Order.Requests.Queries;

public record OrderGetAllQueryReq() : IRequest<List<OrderDto>>;