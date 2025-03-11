using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Application.DTOs.Categury;
using Shop.Domain.Models;

namespace Shop.Application.Profiles
{
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
}
