using MediatR;

using Parstech.Shop.Context.Application.DTOs.UserShipping;

namespace Parstech.Shop.Context.Application.Features.UserShipping.Requests.Commands;

public record UserShippingUpdateCommandReq(UserShippingDto UserShippingDto) : IRequest<UserShippingDto>;