using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.UserShipping.Requests.Commands;

public record UserShippingUpdateCommandReq(UserShippingDto UserShippingDto) : IRequest<UserShippingDto>;