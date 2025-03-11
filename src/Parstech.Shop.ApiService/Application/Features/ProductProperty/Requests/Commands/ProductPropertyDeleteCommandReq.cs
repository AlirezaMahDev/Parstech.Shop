using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Shop.Application.Features.ProductProperty.Requests.Commands
{
    public record ProductPropertyDeleteCommandReq(int id) : IRequest<Unit>;
    
}
