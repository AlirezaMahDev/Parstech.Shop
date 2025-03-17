using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class StatusMapping : Profile
{
    public StatusMapping()
    {
        CreateMap<Status, StatusDto>().ReverseMap();
    }
}