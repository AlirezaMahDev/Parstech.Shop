using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Shop.Application.Features.Property.Requests.Commands
{
    public record PropertyDeleteCommandReq(int id) : IRequest<Unit>;
    
}
