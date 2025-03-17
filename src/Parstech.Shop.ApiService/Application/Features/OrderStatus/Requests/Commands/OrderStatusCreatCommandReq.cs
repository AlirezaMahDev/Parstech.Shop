using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.OrderStatus.Requests.Commands;

public record OrderStatusCreatCommandReq(OrderStatusDto OrderStatusDto) : IRequest<ResponseDto>;