using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Application.DTOs.Section;
using Shop.Application.DTOs.SectionDetail;
using Shop.Domain.Models;

namespace Shop.Application.Profiles
{
    public class SectionAndDetailsMapping:Profile
    {
        public SectionAndDetailsMapping()
        {
            CreateMap<Section, SectionAndDetailsDto>().ReverseMap();
            CreateMap<SectionDetail, SectionAndDetailsDto>().ReverseMap();

            CreateMap<Section, SectionDto>().ReverseMap();
            CreateMap<SectionDetail, SectionDetailDto>().ReverseMap();
            CreateMap<SectionDetail, SectionDetailShowDto>().ReverseMap();
        }
    }
}
