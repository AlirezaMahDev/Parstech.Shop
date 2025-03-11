using AutoMapper;
using Shop.Application.DTOs.ProductRelated;
using Shop.Application.DTOs.ProductRepresentation;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Profiles
{
    public class ProductRelatedMapping : Profile
    {
        public ProductRelatedMapping()
        {
            CreateMap<ProductRelated, ProductRelatedDto>().ReverseMap();
        }
    }
}
