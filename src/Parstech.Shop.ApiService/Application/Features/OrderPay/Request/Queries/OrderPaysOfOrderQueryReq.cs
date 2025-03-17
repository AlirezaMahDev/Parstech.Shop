using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.OrderPay.Request.Queries;

public record OrderPaysOfOrderQueryReq(int orderId) : IRequest<ResponseDto>;