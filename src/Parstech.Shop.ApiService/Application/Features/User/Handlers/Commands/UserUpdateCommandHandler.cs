using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.User.Requests.Commands;

namespace Parstech.Shop.ApiService.Application.Features.User.Handlers.Commands;

public class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommandReq, UserDto>
{
    public Task<UserDto> Handle(UserUpdateCommandReq request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}