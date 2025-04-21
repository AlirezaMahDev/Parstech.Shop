using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Application.DTOs.ProductLog;
using Shop.Domain.Models;

namespace Shop.Application.Profiles
{
    public class PodcuctLogMapping:Profile
    {
        public PodcuctLogMapping()
        {
            CreateMap<ProductLog, ProductLogDto>().ReverseMap();
        }
    }
}
