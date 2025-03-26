using MediatR;

using Parstech.Shop.Context.Application.DTOs.OrderDetail;

namespace Parstech.Shop.Context.Application.Features.OrderDetail.Requests.Commands;

public record OrderDetailReadCommandReq(int id) : IRequest<OrderDetailDto>;