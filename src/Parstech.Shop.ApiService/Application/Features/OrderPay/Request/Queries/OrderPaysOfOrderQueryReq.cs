using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.OrderPay.Request.Queries;

public record OrderPaysOfOrderQueryReq(int orderId) : IRequest<ResponseDto>;