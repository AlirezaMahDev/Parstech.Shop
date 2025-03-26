using AutoMapper;
using Parstech.Shop.Context.Application.DTOs.Tax;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class TaxMapping:Profile
{
    public TaxMapping()
    {
        CreateMap<Tax, TaxDto>().ReverseMap();
    }
}