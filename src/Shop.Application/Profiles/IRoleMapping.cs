using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Application.DTOs.IRole;
using Shop.Application.DTOs.Section;
using Shop.Domain.Models;

namespace Shop.Application.Profiles
{
    public class IRoleMapping:Profile
    {
        public IRoleMapping()
        {
            CreateMap<Irole, IRoleDto>().ReverseMap();
        }
    }
}
