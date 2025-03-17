using AutoMapper;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class StateMapping : Profile
{
    public StateMapping()
    {
        CreateMap<State, SteteDto>().ReverseMap();
    }
}