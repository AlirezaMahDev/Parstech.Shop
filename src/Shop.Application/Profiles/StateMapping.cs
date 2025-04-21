using AutoMapper;
using Shop.Application.DTOs.State;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Profiles
{
    public class StateMapping:Profile
    {
        public StateMapping()
        {
            CreateMap<State, SteteDto>().ReverseMap();
        }
    }
}
