using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class ProductGalleryMapping : Profile
{
    public ProductGalleryMapping()
    {
        CreateMap<ProductGallery, ProductGalleryDto>().ReverseMap();
    }
}