using AutoMapper;
using Shop.Application.DTOs.Wallet;
using Shop.Application.DTOs.WalletTransaction;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Profiles
{
    public class WalletTransactionMapping  : Profile
    {
        public WalletTransactionMapping()
        {
            CreateMap<WalletTransaction, WalletTransactionDto>().ReverseMap();
        }
    }
}
