using MediatR;
using Shop.Application.DTOs.Order;
using Shop.Application.DTOs.OrderDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.OrderDetail.Requests.Commands
{
    public record OrderDetailReadCommandReq(int id) : IRequest<OrderDetailDto>;
}
