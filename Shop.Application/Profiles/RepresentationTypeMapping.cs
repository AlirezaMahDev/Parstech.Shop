using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Application.DTOs.RepresentationType;
using Shop.Domain.Models;

namespace Shop.Application.Profiles
{
    public class RepresentationTypeMapping:Profile

    {
        public RepresentationTypeMapping()
        {
            CreateMap<RepresentationTypeDto, RepresentationType>().ReverseMap();
        }
    }
}
