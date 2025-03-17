using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.OrderPay.Request.Command;

public record OrderPayDeleteCommandReq(int id) : IRequest<ResponseDto>;