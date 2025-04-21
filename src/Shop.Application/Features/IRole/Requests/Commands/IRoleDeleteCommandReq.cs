using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Shop.Application.Features.IRole.Requests.Commands
{
    public record IRoleDeleteCommandReq(string id) : IRequest<Unit>;


}
