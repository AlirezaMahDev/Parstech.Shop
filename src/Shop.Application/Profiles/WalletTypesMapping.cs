using AutoMapper;
using Shop.Application.DTOs.WalletTransaction;
using Shop.Application.DTOs.WalletTypes;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Profiles
{
    public class WalletTypesMapping : Profile
    {
        public WalletTypesMapping()
        {
            CreateMap<WalletType, WalletTypesDto>().ReverseMap();
        }
    }
}
