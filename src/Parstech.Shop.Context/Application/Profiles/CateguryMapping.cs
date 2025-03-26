using AutoMapper;
using Parstech.Shop.Context.Application.DTOs.Categury;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class CateguryMapping:Profile
{
    public CateguryMapping()
    {
        CreateMap<Categury, CateguryDto>().ReverseMap();
        CreateMap<Categury, ParrentCateguryShowDto>().ReverseMap();
        CreateMap<Categury, SubParrentCateguryShowDto>().ReverseMap();
        CreateMap<Categury, SubCateguryShowDto>().ReverseMap();
        CreateMap<CateguryDto, SubCateguryShowDto>().ReverseMap();
    }
}