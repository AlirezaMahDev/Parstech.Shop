using AutoMapper;

using Google.Protobuf.WellKnownTypes;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.Shared.MapperProfiles;

public static class OrderShippingMapperExtensions
{
    public static void AddOrderShippingMappings(this IMapperConfigurationExpression cfg)
    {
        // OrderShipping DTO to Proto mapping
        cfg.CreateMap<OrderShippingDto, OrderShipping>()
            .ForMember(dest => dest.FirstName,
                opt => opt.MapFrom(src => src.FirstName != null ? new StringValue { Value = src.FirstName } : null))
            .ForMember(dest => dest.LastName,
                opt => opt.MapFrom(src => src.LastName != null ? new StringValue { Value = src.LastName } : null))
            .ForMember(dest => dest.Phone,
                opt => opt.MapFrom(src => src.Phone != null ? new StringValue { Value = src.Phone } : null))
            .ForMember(dest => dest.Mobile,
                opt => opt.MapFrom(src => src.Mobile != null ? new StringValue { Value = src.Mobile } : null))
            .ForMember(dest => dest.PostCode,
                opt => opt.MapFrom(src => src.PostCode != null ? new StringValue { Value = src.PostCode } : null))
            .ForMember(dest => dest.FullAddress,
                opt => opt.MapFrom(src =>
                    src.FullAddress != null ? new StringValue { Value = src.FullAddress } : null));

        cfg.CreateMap<OrderShipping, OrderShippingDto>()
            .ForMember(dest => dest.FirstName,
                opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName,
                opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.Mobile, opt => opt.MapFrom(src => src.Mobile))
            .ForMember(dest => dest.PostCode,
                opt => opt.MapFrom(src => src.PostCode))
            .ForMember(dest => dest.FullAddress,
                opt => opt.MapFrom(src => src.FullAddress));

        // Map Parstech.Shop.Shared.Protos.OrderShipping.UserShipping to Shop.Application.DTOs.UserShipping.UserShippingDto and back
        cfg.CreateMap<UserShipping, UserShippingDto>()
            .ForMember(dest => dest.FirstName,
                opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName,
                opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.Mobile, opt => opt.MapFrom(src => src.Mobile))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.PostCode,
                opt => opt.MapFrom(src => src.PostCode));

        cfg.CreateMap<UserShippingDto, UserShipping>()
            .ForMember(dest => dest.FirstName,
                opt => opt.MapFrom(src => src.FirstName != null ? new StringValue { Value = src.FirstName } : null))
            .ForMember(dest => dest.LastName,
                opt => opt.MapFrom(src => src.LastName != null ? new StringValue { Value = src.LastName } : null))
            .ForMember(dest => dest.Phone,
                opt => opt.MapFrom(src => src.Phone != null ? new StringValue { Value = src.Phone } : null))
            .ForMember(dest => dest.Mobile,
                opt => opt.MapFrom(src => src.Mobile != null ? new StringValue { Value = src.Mobile } : null))
            .ForMember(dest => dest.Country,
                opt => opt.MapFrom(src => src.Country != null ? new StringValue { Value = src.Country } : null))
            .ForMember(dest => dest.State,
                opt => opt.MapFrom(src => src.State != null ? new StringValue { Value = src.State } : null))
            .ForMember(dest => dest.City,
                opt => opt.MapFrom(src => src.City != null ? new StringValue { Value = src.City } : null))
            .ForMember(dest => dest.Address,
                opt => opt.MapFrom(src => src.Address != null ? new StringValue { Value = src.Address } : null))
            .ForMember(dest => dest.PostCode,
                opt => opt.MapFrom(src => src.PostCode != null ? new StringValue { Value = src.PostCode } : null));
    }
}