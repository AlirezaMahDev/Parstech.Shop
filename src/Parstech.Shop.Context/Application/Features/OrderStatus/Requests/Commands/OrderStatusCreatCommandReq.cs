using MediatR;

using Parstech.Shop.Context.Application.DTOs.OrderStatus;
using Parstech.Shop.Context.Application.DTOs.Response;

namespace Parstech.Shop.Context.Application.Features.OrderStatus.Requests.Commands;

public record OrderStatusCreatCommandReq(OrderStatusDto OrderStatusDto) : IRequest<ResponseDto>;