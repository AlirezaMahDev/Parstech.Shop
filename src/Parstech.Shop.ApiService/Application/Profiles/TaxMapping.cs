using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class TaxMapping : Profile
{
    public TaxMapping()
    {
        CreateMap<Tax, TaxDto>().ReverseMap();
    }
}