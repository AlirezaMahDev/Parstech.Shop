using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Application.DTOs.Tax;
using Shop.Domain.Models;

namespace Shop.Application.Profiles
{
    public class TaxMapping:Profile
    {
        public TaxMapping()
        {
            CreateMap<Tax, TaxDto>().ReverseMap();
        }
    }
}
