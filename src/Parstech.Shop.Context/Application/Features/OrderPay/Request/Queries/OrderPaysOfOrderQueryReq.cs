using MediatR;
using Parstech.Shop.Context.Application.DTOs.Response;

namespace Parstech.Shop.Context.Application.Features.OrderPay.Request.Queries;

public record OrderPaysOfOrderQueryReq(int orderId):IRequest<ResponseDto>;