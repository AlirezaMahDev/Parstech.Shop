using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Application.DTOs.Wallet;
using Shop.Domain.Models;

namespace Shop.Application.Profiles
{
    public class WalletMapping:Profile
    {
        public WalletMapping()
        {
            CreateMap<Wallet, WalletDto>().ReverseMap();
        }
    }
}
