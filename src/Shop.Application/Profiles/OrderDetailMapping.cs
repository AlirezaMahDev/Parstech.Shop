using AutoMapper;
using Shop.Application.DTOs.Order;
using Shop.Application.DTOs.OrderDetail;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Profiles
{
    public class OrderDetailMapping : Profile
    {
        public OrderDetailMapping() 
        {
            CreateMap<OrderDetail, OrderDetailDto>().ReverseMap();
        }

    }
}
