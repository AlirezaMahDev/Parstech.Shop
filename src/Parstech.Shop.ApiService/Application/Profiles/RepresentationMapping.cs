using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Application.DTOs.Representation;
using Shop.Domain.Models;

namespace Shop.Application.Profiles
{
    public class RepresentationMapping:Profile
    {
        public RepresentationMapping()
        {
            CreateMap<Representation, RepresentationDto>().ReverseMap();
        }
    }
}
