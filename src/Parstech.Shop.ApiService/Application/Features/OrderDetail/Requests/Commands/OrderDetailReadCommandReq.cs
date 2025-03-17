using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.OrderDetail.Requests.Commands;

public record OrderDetailReadCommandReq(int id) : IRequest<OrderDetailDto>;