using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class RahkaranOrderMapping : Profile
{
    public RahkaranOrderMapping()
    {
        CreateMap<RahkaranOrder, RahkaranOrderDto>().ReverseMap();
    }
}