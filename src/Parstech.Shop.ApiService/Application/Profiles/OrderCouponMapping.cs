using AutoMapper;
using Shop.Application.DTOs.OrderCoupon;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Profiles
{
    public class OrderCouponMapping:Profile
    {
        public OrderCouponMapping()
        {
            CreateMap<OrderCoupon,OrderCouponDto>().ReverseMap();
        }
    }
}
