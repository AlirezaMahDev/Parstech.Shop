using AutoMapper;
using Shop.Application.DTOs.OrderPay;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Profiles
{
    public class OrderPayMapping:Profile
    {
        public OrderPayMapping()
        {
            CreateMap<OrderPay,OrderPayDto>().ReverseMap();
        }
    }
}
