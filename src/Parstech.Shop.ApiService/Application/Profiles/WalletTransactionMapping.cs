using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class WalletTransactionMapping : Profile
{
    public WalletTransactionMapping()
    {
        CreateMap<WalletTransaction, WalletTransactionDto>().ReverseMap();
    }
}