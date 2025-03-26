using AutoMapper;

using Parstech.Shop.Context.Application.DTOs.ProductRepresentation;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class ProductRepresemtationMapping : Profile
{
    public ProductRepresemtationMapping()
    {
        CreateMap<ProductRepresentation, ProductRepresentationDto>().ReverseMap();
    }

}