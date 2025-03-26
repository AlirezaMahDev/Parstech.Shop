using AutoMapper;
using Parstech.Shop.Context.Application.DTOs.UserProduct;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class UserProductMapping : Profile
{
    public UserProductMapping()
    {
        CreateMap<UserProduct, UserProductDto>().ReverseMap();
    }
}