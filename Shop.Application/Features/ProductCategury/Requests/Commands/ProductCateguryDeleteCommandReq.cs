using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Shop.Application.Features.ProductCategury.Requests.Commands
{
    public record ProductCateguryDeleteCommandReq(int id) : IRequest<int>;
    
}
