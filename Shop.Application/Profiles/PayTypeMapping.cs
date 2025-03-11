using AutoMapper;
using Shop.Application.DTOs.PayType;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Profiles
{
    public class PayTypeMapping:Profile
    {
        public PayTypeMapping()
        {
            CreateMap<PayType,PayTypeDto>().ReverseMap();
        }
    }
}
