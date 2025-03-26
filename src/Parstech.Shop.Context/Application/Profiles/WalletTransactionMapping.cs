using AutoMapper;

using Parstech.Shop.Context.Application.DTOs.WalletTransaction;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class WalletTransactionMapping  : Profile
{
    public WalletTransactionMapping()
    {
        CreateMap<WalletTransaction, WalletTransactionDto>().ReverseMap();
    }
}