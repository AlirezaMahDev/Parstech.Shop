using MediatR;
using Parstech.Shop.Context.Application.DTOs.Response;

namespace Parstech.Shop.Context.Application.Features.OrderPay.Request.Queries;

public record AddOrUpdateOrderPayQueryReq(int orderId,int? PayTypeId,int? PayStatysId,string? Description) :IRequest<ResponseDto>;