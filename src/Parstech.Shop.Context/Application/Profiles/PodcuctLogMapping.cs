using AutoMapper;
using Parstech.Shop.Context.Application.DTOs.ProductLog;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class PodcuctLogMapping:Profile
{
    public PodcuctLogMapping()
    {
        CreateMap<ProductLog, ProductLogDto>().ReverseMap();
    }
}