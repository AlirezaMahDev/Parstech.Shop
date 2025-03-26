using MediatR;

using Parstech.Shop.Context.Application.DTOs.UserShipping;

namespace Parstech.Shop.Context.Application.Features.UserShipping.Requests.Commands;

public record UserShippingCreateCommandReq(UserShippingDto UserShippingDto) : IRequest<UserShippingDto>;