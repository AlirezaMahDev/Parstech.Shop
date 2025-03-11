using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Application.DTOs.OrderDetail;
using Shop.Application.DTOs.ProductRepresentation;
using Shop.Domain.Models;

namespace Shop.Application.Profiles
{
    public class ProductRepresemtationMapping : Profile
    {
        public ProductRepresemtationMapping()
        {
            CreateMap<ProductRepresentation, ProductRepresentationDto>().ReverseMap();
        }

    }
}
