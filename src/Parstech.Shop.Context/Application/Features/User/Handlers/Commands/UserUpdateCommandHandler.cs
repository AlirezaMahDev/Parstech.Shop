using MediatR;

using Parstech.Shop.Context.Application.DTOs.User;
using Parstech.Shop.Context.Application.Features.User.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.User.Handlers.Commands;

public class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommandReq, UserDto>
{
    public Task<UserDto> Handle(UserUpdateCommandReq request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}