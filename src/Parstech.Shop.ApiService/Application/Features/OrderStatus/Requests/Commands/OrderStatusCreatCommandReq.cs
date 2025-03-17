using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.OrderStatus.Requests.Commands;

public record OrderStatusCreatCommandReq(OrderStatusDto OrderStatusDto) : IRequest<ResponseDto>;