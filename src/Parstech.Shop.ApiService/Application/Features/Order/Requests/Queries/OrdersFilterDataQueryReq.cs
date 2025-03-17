using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.Order.Requests.Queries;

public record OrdersFilterDataQueryReq(string userName) : IRequest<OrderFilterDto>;