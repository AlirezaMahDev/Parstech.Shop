using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.OrderStatus.Requests.Queries
{
    public record CreateOrderStatusByStatusIdQueryReq(string Latinstatus,int orderId,int userId):IRequest<Unit>;

}
