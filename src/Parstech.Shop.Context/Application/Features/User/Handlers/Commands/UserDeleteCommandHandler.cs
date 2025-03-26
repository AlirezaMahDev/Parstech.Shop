using MediatR;

using Parstech.Shop.Context.Application.Features.User.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.User.Handlers.Commands;

public class UserDeleteCommandHandler : IRequestHandler<UserDeleteCommandReq, Unit>
{
    public Task<Unit> Handle(UserDeleteCommandReq request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}