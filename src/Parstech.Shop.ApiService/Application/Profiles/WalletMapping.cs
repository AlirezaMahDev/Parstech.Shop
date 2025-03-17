using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class WalletMapping : Profile
{
    public WalletMapping()
    {
        CreateMap<Wallet, WalletDto>().ReverseMap();
    }
}