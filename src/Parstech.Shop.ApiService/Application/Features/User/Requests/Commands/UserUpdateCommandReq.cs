using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.User.Requests.Commands;

public record UserUpdateCommandReq(UserDto userDto) : IRequest<UserDto>;