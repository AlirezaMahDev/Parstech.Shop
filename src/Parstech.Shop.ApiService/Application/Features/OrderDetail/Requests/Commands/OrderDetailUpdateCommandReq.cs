using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.OrderDetail.Requests.Commands;

public record OrderDetailUpdateCommandReq(OrderDetailDto OrderDetailDto) : IRequest<OrderDetailDto>;