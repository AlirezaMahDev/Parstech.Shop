using AutoMapper;
using Parstech.Shop.Context.Application.DTOs.FormCredit;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class FormCreditMapping:Profile
{
    public FormCreditMapping()
    {
        CreateMap<FormCredit,FormCreditDto>().ReverseMap();
    }
}