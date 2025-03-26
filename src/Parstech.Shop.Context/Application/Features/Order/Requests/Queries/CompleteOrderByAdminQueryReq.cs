using MediatR;

using Parstech.Shop.Context.Application.DTOs.Response;

namespace Parstech.Shop.Context.Application.Features.Order.Requests.Queries;

public record CompleteOrderByAdminQueryReq(int orderId,string typeName, int? month):IRequest<ResponseDto>;