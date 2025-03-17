using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.Order.Requests.Queries;

public record RefreshOrderQueryReq(int id) : IRequest<OrderResponse>;