﻿using MediatR;
using Shop.Application.DTOs.Order;
using Shop.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Order.Requests.Commands
{
    public record OrderCreateCommandReq(OrderDto OrderDto) : IRequest<OrderDto>;
}
