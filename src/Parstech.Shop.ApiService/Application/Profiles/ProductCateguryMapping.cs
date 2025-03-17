using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class ProductCateguryMapping : Profile
{
    public ProductCateguryMapping()
    {
        CreateMap<ProductCategury, ProductCateguryDto>().ReverseMap();
    }
}