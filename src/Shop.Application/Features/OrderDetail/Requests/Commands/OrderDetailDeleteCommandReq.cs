using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.OrderDetail.Requests.Commands
{
    public record OrderDetailDeleteCommandReq(int Id):IRequest<Unit>;

}
