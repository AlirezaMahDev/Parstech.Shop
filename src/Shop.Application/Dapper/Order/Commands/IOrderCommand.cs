﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Dapper.Order.Commands
{
    public interface IOrderCommand
    {
        string GetOrderByOrderCode { get; }
    }
}
