using MediatR;
using Parstech.Shop.Context.Application.DTOs.OrderPay;
using Parstech.Shop.Context.Application.DTOs.Response;

namespace Parstech.Shop.Context.Application.Features.OrderPay.Request.Command;

public record OrderPayCreateCommandReq(OrderPayDto orderPayDto):IRequest<ResponseDto>;