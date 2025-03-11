using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Application.DTOs.PropertyCategury;
using Shop.Domain.Models;

namespace Shop.Application.Profiles
{
    public class PropertyCateguryMapping:Profile
    {
        public PropertyCateguryMapping()
        {
            CreateMap<PropertyCategury, PropertyCateguryDto>().ReverseMap();
        }
    }
}
