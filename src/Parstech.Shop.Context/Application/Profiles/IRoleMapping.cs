using AutoMapper;
using Parstech.Shop.Context.Application.DTOs.IRole;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class IRoleMapping:Profile
{
    public IRoleMapping()
    {
        CreateMap<Irole, IRoleDto>().ReverseMap();
    }
}