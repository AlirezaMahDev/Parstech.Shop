using MediatR;
using Shop.Application.DTOs.OrderShipping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.OrderShipping.Request.Queries
{
    public record OrderShippingGetByOrderIdQueryReq(int orderId):IRequest<OrderShippingDto>;
}
