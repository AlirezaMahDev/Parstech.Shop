using MediatR;
using Parstech.Shop.Context.Application.DTOs.Response;

namespace Parstech.Shop.Context.Application.Features.OrderPay.Request.Command;

public record OrderPayDeleteCommandReq(int id):IRequest<ResponseDto>;