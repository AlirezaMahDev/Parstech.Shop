using AutoMapper;
using Parstech.Shop.Context.Application.DTOs.Status;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class StatusMapping:Profile
{
    public StatusMapping()
    {
        CreateMap<Status, StatusDto>().ReverseMap();
    }
}