using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class FormCreditMapping : Profile
{
    public FormCreditMapping()
    {
        CreateMap<FormCredit, FormCreditDto>().ReverseMap();
    }
}