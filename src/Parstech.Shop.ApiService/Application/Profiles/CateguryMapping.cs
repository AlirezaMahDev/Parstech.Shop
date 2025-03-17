using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class CateguryMapping : Profile
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