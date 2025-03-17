using MediatR;

using Parstech.Shop.ApiService.Application.Features.User.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.User.Handlers.Commands;

public class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommandReq, UserDto>
{
    public Task<UserDto> Handle(UserUpdateCommandReq request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}