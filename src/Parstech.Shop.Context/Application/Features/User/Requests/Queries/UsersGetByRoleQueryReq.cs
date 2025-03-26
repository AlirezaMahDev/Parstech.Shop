using MediatR;

using Parstech.Shop.Context.Application.DTOs.User;

namespace Parstech.Shop.Context.Application.Features.User.Requests.Queries;

public record UsersGetByRoleQueryReq(string role):IRequest<List<UserDto>>;