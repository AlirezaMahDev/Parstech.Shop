using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class ProductRepresemtationMapping : Profile
{
    public ProductRepresemtationMapping()
    {
        CreateMap<ProductRepresentation, ProductRepresentationDto>().ReverseMap();
    }
}