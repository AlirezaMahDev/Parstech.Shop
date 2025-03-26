using AutoMapper;

using Parstech.Shop.Context.Application.DTOs.Rahkaran;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class RahkaranUserMapping : Profile
{
    public RahkaranUserMapping()
    {
        CreateMap<RahkaranUser, RahkaranUserDto>().ReverseMap();
    }
}