using AutoMapper;
using Shop.Application.DTOs.Brand;
using Shop.Application.DTOs.CreditProductStockPrice;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Profiles
{
    public class CreditProductStockPriceMapping:Profile
    {
        public CreditProductStockPriceMapping()
        {
            CreateMap<CreditProductStockPrice, CreditProductStockPriceDto>().ReverseMap();
        }
    }
}
