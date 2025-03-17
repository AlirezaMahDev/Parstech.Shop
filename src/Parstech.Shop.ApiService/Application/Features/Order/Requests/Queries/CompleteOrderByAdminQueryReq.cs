using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Order.Requests.Queries;

public record CompleteOrderByAdminQueryReq(int orderId, string typeName, int? month) : IRequest<ResponseDto>;