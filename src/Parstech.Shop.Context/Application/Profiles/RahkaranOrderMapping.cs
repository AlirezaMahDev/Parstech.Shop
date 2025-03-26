using AutoMapper;

using Parstech.Shop.Context.Application.DTOs.Rahkaran;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class RahkaranOrderMapping : Profile
{
    public RahkaranOrderMapping()
    {
        CreateMap<RahkaranOrder, RahkaranOrderDto>().ReverseMap();
    }
}