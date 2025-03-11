using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.User;
using Shop.Application.Features.User.Requests.Commands;

namespace Shop.Application.Features.User.Handlers.Commands
{
    public class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommandReq, UserDto>
    {
        public Task<UserDto> Handle(UserUpdateCommandReq request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
