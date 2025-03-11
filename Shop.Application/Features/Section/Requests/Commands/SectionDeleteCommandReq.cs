using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Shop.Application.Features.Section.Requests.Commands
{
    public record SectionDeleteCommandReq(int id) : IRequest<Unit>;
}
