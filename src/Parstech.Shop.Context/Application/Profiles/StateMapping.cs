using AutoMapper;
using Parstech.Shop.Context.Application.DTOs.State;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class StateMapping:Profile
{
    public StateMapping()
    {
        CreateMap<State, SteteDto>().ReverseMap();
    }
}