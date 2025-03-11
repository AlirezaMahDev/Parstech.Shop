using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Shop.Application.Features.Product.Requests.Commands
{
    public record ProductDeleteCommandReq(int id) : IRequest<Unit>;
}
