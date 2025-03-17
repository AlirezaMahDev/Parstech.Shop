using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class StateMapping : Profile
{
    public StateMapping()
    {
        CreateMap<State, SteteDto>().ReverseMap();
    }
}