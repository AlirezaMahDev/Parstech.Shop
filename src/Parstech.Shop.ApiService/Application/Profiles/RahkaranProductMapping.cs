using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class RahkaranProductMapping : Profile
{
    public RahkaranProductMapping()
    {
        CreateMap<RahkaranProduct, RahkaranProductDto>().ReverseMap();
    }
}