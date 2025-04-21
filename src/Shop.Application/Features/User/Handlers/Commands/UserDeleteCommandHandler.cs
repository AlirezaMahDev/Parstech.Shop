using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.Features.User.Requests.Commands;

namespace Shop.Application.Features.User.Handlers.Commands
{
    public class UserDeleteCommandHandler : IRequestHandler<UserDeleteCommandReq, Unit>
    {
        public Task<Unit> Handle(UserDeleteCommandReq request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
