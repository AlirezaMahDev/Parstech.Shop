using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.UserShipping.Requests.Commands;

public record UserShippingCreateCommandReq(UserShippingDto UserShippingDto) : IRequest<UserShippingDto>;