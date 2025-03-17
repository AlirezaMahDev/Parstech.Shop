using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class IRoleMapping : Profile
{
    public IRoleMapping()
    {
        CreateMap<Irole, IRoleDto>().ReverseMap();
    }
}