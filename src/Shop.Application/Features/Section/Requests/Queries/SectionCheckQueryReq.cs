using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Shop.Application.Features.Section.Requests.Queries
{
    public record SectionCheckQueryReq(int id) : IRequest<bool>;
}
