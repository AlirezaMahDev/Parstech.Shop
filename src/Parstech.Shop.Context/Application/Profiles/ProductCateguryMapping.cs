using AutoMapper;
using Parstech.Shop.Context.Application.DTOs.ProductCategury;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class ProductCateguryMapping:Profile
{
    public ProductCateguryMapping()
    {
        CreateMap<ProductCategury, ProductCateguryDto>().ReverseMap();
    }
}