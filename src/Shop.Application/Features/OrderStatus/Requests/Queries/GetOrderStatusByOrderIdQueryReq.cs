using MediatR;
using Shop.Application.DTOs.OrderStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.OrderStatus.Requests.Queries
{
    public record GetOrderStatusByOrderIdQueryReq(int orderId) : IRequest<List<StatusOfOrderDto>>;
}
