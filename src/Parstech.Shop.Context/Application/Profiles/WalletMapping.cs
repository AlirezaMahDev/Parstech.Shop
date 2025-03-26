using AutoMapper;
using Parstech.Shop.Context.Application.DTOs.Wallet;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class WalletMapping:Profile
{
    public WalletMapping()
    {
        CreateMap<Wallet, WalletDto>().ReverseMap();
    }
}