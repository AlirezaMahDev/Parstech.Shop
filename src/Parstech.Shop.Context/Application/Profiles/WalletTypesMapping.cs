using AutoMapper;

using Parstech.Shop.Context.Application.DTOs.WalletTypes;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class WalletTypesMapping : Profile
{
    public WalletTypesMapping()
    {
        CreateMap<WalletType, WalletTypesDto>().ReverseMap();
    }
}