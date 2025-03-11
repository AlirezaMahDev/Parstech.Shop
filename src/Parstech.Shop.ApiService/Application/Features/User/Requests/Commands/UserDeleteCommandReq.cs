using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Shop.Application.Features.User.Requests.Commands
{
    public record UserDeleteCommandReq(int id) : IRequest<Unit>;
}
