using MediatR;

namespace Parstech.Shop.Context.Application.Features.OrderStatus.Requests.Queries;

public record CreateOrderStatusByStatusIdQueryReq(string Latinstatus,int orderId,int userId):IRequest<Unit>;