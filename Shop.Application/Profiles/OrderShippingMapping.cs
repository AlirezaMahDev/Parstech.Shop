using AutoMapper;
using Shop.Application.DTOs.OrderShipping;
using Shop.Application.DTOs.OrderStatus;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Profiles
{
	public class OrderShippingMapping:Profile
	{
		public OrderShippingMapping()
		{
			CreateMap<OrderShipping, OrderShippingDto>().ReverseMap();
		}
	}
}
