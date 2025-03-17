using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.OrderPay.Request.Queries;

public record AddOrUpdateOrderPayQueryReq(int orderId, int? PayTypeId, int? PayStatysId, string? Description)
    : IRequest<ResponseDto>;