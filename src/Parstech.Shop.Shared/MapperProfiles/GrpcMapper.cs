using AutoMapper;

using Google.Protobuf.WellKnownTypes;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.Shared.MapperProfiles;

public class GrpcMapper : Profile
{
    public GrpcMapper()
    {
        // Product DTO to Proto mapping
        CreateMap<ProductDto, Product>()
            .ForMember(dest => dest.LatinName,
                opt => opt.MapFrom(src => src.LatinName != null ? new StringValue { Value = src.LatinName } : null))
            .ForMember(dest => dest.Code,
                opt => opt.MapFrom(src => src.Code != null ? new StringValue { Value = src.Code } : null))
            .ForMember(dest => dest.Description,
                opt => opt.MapFrom(src => src.Description != null ? new StringValue { Value = src.Description } : null))
            .ForMember(dest => dest.ShortDescription,
                opt => opt.MapFrom(src =>
                    src.ShortDescription != null ? new StringValue { Value = src.ShortDescription } : null))
            .ForMember(dest => dest.ShortLink,
                opt => opt.MapFrom(src => src.ShortLink != null ? new StringValue { Value = src.ShortLink } : null))
            .ForMember(dest => dest.VariationName,
                opt => opt.MapFrom(src =>
                    src.VariationName != null ? new StringValue { Value = src.VariationName } : null))
            .ForMember(dest => dest.ParentId,
                opt => opt.MapFrom(src => src.ParentId.HasValue ? new Int32Value { Value = src.ParentId.Value } : null))
            .ForMember(dest => dest.TaxCode,
                opt => opt.MapFrom(src => src.TaxCode != null ? new StringValue { Value = src.TaxCode } : null))
            .ForMember(dest => dest.Keywords,
                opt => opt.MapFrom(src => src.Keywords != null ? new StringValue { Value = src.Keywords } : null))
            .ForMember(dest => dest.BestStockId,
                opt => opt.MapFrom(src =>
                    src.BestStockId.HasValue ? new Int32Value { Value = src.BestStockId.Value } : null))
            .ForMember(dest => dest.BestStockUserCateguryId,
                opt => opt.MapFrom(src =>
                    src.BestStockUserCateguryId.HasValue
                        ? new Int32Value { Value = src.BestStockUserCateguryId.Value }
                        : null))
            .ForMember(dest => dest.CreateDate,
                opt => opt.MapFrom(
                    src => Timestamp.FromDateTime(DateTime.SpecifyKind(src.CreateDate, DateTimeKind.Utc))));

        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.LatinName, opt => opt.MapFrom(src => src.LatinName))
            .ForMember(dest => dest.Code, opt => opt.Ignore())
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.ShortDescription))
            .ForMember(dest => dest.ShortLink, opt => opt.MapFrom(src => src.ShortLink))
            .ForMember(dest => dest.VariationName, opt => opt.MapFrom(src => src.VariationName))
            .ForMember(dest => dest.ParentId,
                opt => opt.MapFrom(src => src.ParentId != null ? (int?)src.ParentId.Value : null))
            .ForMember(dest => dest.TaxCode, opt => opt.Ignore())
            .ForMember(dest => dest.Keywords, opt => opt.Ignore())
            .ForMember(dest => dest.BestStockId,
                opt => opt.MapFrom(src => src.BestStockId != null ? (int?)src.BestStockId.Value : null))
            .ForMember(dest => dest.BestStockUserCateguryId,
                opt => opt.MapFrom(src =>
                    src.BestStockUserCateguryId != null ? (int?)src.BestStockUserCateguryId.Value : null))
            .ForMember(dest => dest.CreateDate, opt => opt.Ignore())
            .ForMember(dest => dest.QuantityPerBundle, opt => opt.Ignore())
            .ForMember(dest => dest.ShowInDiscountPanels, opt => opt.Ignore())
            .ForMember(dest => dest.CateguryOfUserId, opt => opt.Ignore())
            .ForMember(dest => dest.CateguryOfUserName, opt => opt.Ignore())
            .ForMember(dest => dest.CateguryOfUserType, opt => opt.Ignore())
            .ForMember(dest => dest.DiscountDate, opt => opt.Ignore());

        // Comment out other Product-related mappings that reference undefined types
        /*
        CreateMap<ProductParameterDto, ProductParameter>().ReverseMap();
        CreateMap<ProductSearchParameterDto, ProductSearchParameter>().ReverseMap();
        CreateMap<ProductListShowDto, ProductListShow>().ReverseMap();
        CreateMap<ProductDetailShowDto, ProductDetailShow>().ReverseMap();
        */

        // Comment out all remaining problematic mappings including Order and User mappings
        /*
        // Order DTO to Proto mapping
        CreateMap<OrderDto, Order>()
            .ForMember(dest => dest.CreateDate,
                opt => opt.MapFrom(src => Timestamp.FromDateTime(DateTime.SpecifyKind(src.CreateDate, DateTimeKind.Utc))))
            .ForMember(dest => dest.IntroCode,
                opt => opt.MapFrom(src => src.IntroCode != null ? new StringValue { Value = src.IntroCode } : null))
            .ForMember(dest => dest.ConfirmPayment,
                opt => opt.MapFrom(src =>
                    src.ConfirmPayment.HasValue ? new BoolValue { Value = src.ConfirmPayment.Value } : null))
            .ForMember(dest => dest.FactorFile,
                opt => opt.MapFrom(src => src.FactorFile != null ? new StringValue { Value = src.FactorFile } : null));

        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDate.ToDateTime()))
            .ForMember(dest => dest.IntroCode,
                opt => opt.MapFrom(src => src.IntroCode != null ? src.IntroCode : null))
            .ForMember(dest => dest.ConfirmPayment,
                opt => opt.MapFrom(src => src.ConfirmPayment != null ? (bool?)src.ConfirmPayment : null))
            .ForMember(dest => dest.FactorFile,
                opt => opt.MapFrom(src => src.FactorFile != null ? src.FactorFile : null));

        // User DTO to Proto mapping
        CreateMap<UserDto, User>()
            .ForMember(dest => dest.FirstName,
                opt => opt.MapFrom(src => src.FirstName != null ? new StringValue { Value = src.FirstName } : null))
            .ForMember(dest => dest.LastName,
                opt => opt.MapFrom(src => src.LastName != null ? new StringValue { Value = src.LastName } : null))
            .ForMember(dest => dest.FullName,
                opt => opt.MapFrom(src => src.FullName != null ? new StringValue { Value = src.FullName } : null))
            .ForMember(dest => dest.UserId,
                opt => opt.MapFrom(src => src.UserId != null ? new StringValue { Value = src.UserId } : null))
            .ForMember(dest => dest.Avatar,
                opt => opt.MapFrom(src => src.Avatar != null ? new StringValue { Value = src.Avatar } : null))
            .ForMember(dest => dest.LastLoginDate,
                opt => opt.MapFrom(src =>
                    Timestamp.FromDateTime(DateTime.SpecifyKind(src.LastLoginDate, DateTimeKind.Utc))))
            .ForMember(dest => dest.IsDelete,
                opt => opt.MapFrom(src => src.IsDelete.HasValue ? new BoolValue { Value = src.IsDelete.Value } : null));

        CreateMap<User, UserDto>()
            .ForMember(dest => dest.FirstName,
                opt => opt.MapFrom(src => src.FirstName != null ? src.FirstName.Value : null))
            .ForMember(dest => dest.LastName,
                opt => opt.MapFrom(src => src.LastName != null ? src.LastName.Value : null))
            .ForMember(dest => dest.FullName,
                opt => opt.MapFrom(src => src.FullName != null ? src.FullName.Value : null))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId != null ? src.UserId.Value : null))
            .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Avatar != null ? src.Avatar.Value : null))
            .ForMember(dest => dest.LastLoginDate, opt => opt.MapFrom(src => src.LastLoginDate.ToDateTime()))
            .ForMember(dest => dest.IsDelete,
                opt => opt.MapFrom(src => src.IsDelete != null ? (bool?)src.IsDelete.Value : null));

        // All other mappers go here... they are all commented out to avoid errors.
        */
    }
}