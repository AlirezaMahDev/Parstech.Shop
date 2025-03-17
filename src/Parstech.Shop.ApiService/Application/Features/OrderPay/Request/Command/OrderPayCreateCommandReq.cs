using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.OrderPay.Request.Command;

public record OrderPayCreateCommandReq(OrderPayDto orderPayDto) : IRequest<ResponseDto>;