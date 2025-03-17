using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class WalletTypesMapping : Profile
{
    public WalletTypesMapping()
    {
        CreateMap<WalletType, WalletTypesDto>().ReverseMap();
    }
}