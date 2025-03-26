using AutoMapper;
using Parstech.Shop.Context.Application.DTOs.ProductGallery;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class ProductGalleryMapping:Profile
{
    public ProductGalleryMapping()
    {
        CreateMap<ProductGallery, ProductGalleryDto>().ReverseMap();
    }
}