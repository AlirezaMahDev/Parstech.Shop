using AutoMapper;
using Shop.Application.DTOs.CouponPcu;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Profiles
{
	public class CouponPcuMapping:Profile
	{
        public CouponPcuMapping()
        {
            CreateMap<CouponPcu, CouponPcuDto>().ReverseMap();
            CreateMap<CouponPcu, CouponCheckPcuDto>().ReverseMap();
        }
    }
}
