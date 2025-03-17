using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.User.Requests.Commands;

public record UserReadCommandReq(int id) : IRequest<UserDto>;