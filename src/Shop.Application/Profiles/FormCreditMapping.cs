using AutoMapper;
using Shop.Application.DTOs.FormCredit;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Profiles
{
    public class FormCreditMapping:Profile
    {
        public FormCreditMapping()
        {
            CreateMap<FormCredit,FormCreditDto>().ReverseMap();
        }
    }
}
