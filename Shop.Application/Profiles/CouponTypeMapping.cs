using AutoMapper;
using Shop.Application.DTOs.Coupon;
using Shop.Application.DTOs.CouponType;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Profiles
{
    public class CouponTypeMapping:Profile
    {
        public CouponTypeMapping()
        {
            CreateMap<CouponType, CouponTypeDto>().ReverseMap();
        }
    }
}
