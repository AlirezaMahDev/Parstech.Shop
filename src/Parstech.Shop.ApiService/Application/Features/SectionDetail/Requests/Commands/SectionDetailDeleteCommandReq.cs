using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Shop.Application.Features.SectionDetail.Requests.Commands
{
    public record SectionDetailDeleteCommandReq(int id) : IRequest<Unit>;
}
