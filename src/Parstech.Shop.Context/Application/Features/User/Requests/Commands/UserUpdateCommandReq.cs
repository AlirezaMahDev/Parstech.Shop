using MediatR;

using Parstech.Shop.Context.Application.DTOs.User;

namespace Parstech.Shop.Context.Application.Features.User.Requests.Commands;

public record UserUpdateCommandReq(UserDto userDto) : IRequest<UserDto>;