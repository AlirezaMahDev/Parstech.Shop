using AutoMapper;
using Parstech.Shop.Context.Application.DTOs.Rahkaran;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class RahkaranProductMapping : Profile
{
    public RahkaranProductMapping()
    {
        CreateMap<RahkaranProduct, RahkaranProductDto>().ReverseMap();
    }
}