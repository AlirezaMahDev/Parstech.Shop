using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Parstech.Shop.Shared.Protos.Order;
using Parstech.Shop.Shared.Protos.Product;
using Parstech.Shop.Shared.Protos.User;
using Parstech.Shop.Shared.Protos.UserShipping;
using Shop.Application.DTOs.Order;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.User;
using Shop.Application.DTOs.UserShipping;
using System;

namespace Parstech.Shop.ApiService.MapperProfiles
{
    public class GrpcMapper : Profile
    {
        public GrpcMapper()
        {
            // Product DTO to Proto mapping
            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.LatinName, opt => opt.MapFrom(src => src.LatinName != null ? new StringValue { Value = src.LatinName } : null))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code != null ? new StringValue { Value = src.Code } : null))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description != null ? new StringValue { Value = src.Description } : null))
                .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.ShortDescription != null ? new StringValue { Value = src.ShortDescription } : null))
                .ForMember(dest => dest.ShortLink, opt => opt.MapFrom(src => src.ShortLink != null ? new StringValue { Value = src.ShortLink } : null))
                .ForMember(dest => dest.VariationName, opt => opt.MapFrom(src => src.VariationName != null ? new StringValue { Value = src.VariationName } : null))
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId.HasValue ? new Int32Value { Value = src.ParentId.Value } : null))
                .ForMember(dest => dest.QuantityPerBundle, opt => opt.MapFrom(src => src.QuantityPerBundle.HasValue ? new Int32Value { Value = src.QuantityPerBundle.Value } : null))
                .ForMember(dest => dest.TaxCode, opt => opt.MapFrom(src => src.TaxCode != null ? new StringValue { Value = src.TaxCode } : null))
                .ForMember(dest => dest.Keywords, opt => opt.MapFrom(src => src.Keywords != null ? new StringValue { Value = src.Keywords } : null))
                .ForMember(dest => dest.ShowInDiscountPanels, opt => opt.MapFrom(src => src.ShowInDiscountPanels.HasValue ? new BoolValue { Value = src.ShowInDiscountPanels.Value } : null))
                .ForMember(dest => dest.CateguryOfUserId, opt => opt.MapFrom(src => src.CateguryOfUserId.HasValue ? new Int32Value { Value = src.CateguryOfUserId.Value } : null))
                .ForMember(dest => dest.BestStockId, opt => opt.MapFrom(src => src.BestStockId.HasValue ? new Int32Value { Value = src.BestStockId.Value } : null))
                .ForMember(dest => dest.BestStockUserCateguryId, opt => opt.MapFrom(src => src.BestStockUserCateguryId.HasValue ? new Int32Value { Value = src.BestStockUserCateguryId.Value } : null))
                .ForMember(dest => dest.DiscountDate, opt => opt.MapFrom(src => src.DiscountDate.HasValue ? Timestamp.FromDateTime(DateTime.SpecifyKind(src.DiscountDate.Value, DateTimeKind.Utc)) : null))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => Timestamp.FromDateTime(DateTime.SpecifyKind(src.CreateDate, DateTimeKind.Utc))));

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.LatinName, opt => opt.MapFrom(src => src.LatinName != null ? src.LatinName.Value : null))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code != null ? src.Code.Value : null))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description != null ? src.Description.Value : null))
                .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.ShortDescription != null ? src.ShortDescription.Value : null))
                .ForMember(dest => dest.ShortLink, opt => opt.MapFrom(src => src.ShortLink != null ? src.ShortLink.Value : null))
                .ForMember(dest => dest.VariationName, opt => opt.MapFrom(src => src.VariationName != null ? src.VariationName.Value : null))
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId != null ? (int?)src.ParentId.Value : null))
                .ForMember(dest => dest.QuantityPerBundle, opt => opt.MapFrom(src => src.QuantityPerBundle != null ? (int?)src.QuantityPerBundle.Value : null))
                .ForMember(dest => dest.TaxCode, opt => opt.MapFrom(src => src.TaxCode != null ? src.TaxCode.Value : null))
                .ForMember(dest => dest.Keywords, opt => opt.MapFrom(src => src.Keywords != null ? src.Keywords.Value : null))
                .ForMember(dest => dest.ShowInDiscountPanels, opt => opt.MapFrom(src => src.ShowInDiscountPanels != null ? (bool?)src.ShowInDiscountPanels.Value : null))
                .ForMember(dest => dest.CateguryOfUserId, opt => opt.MapFrom(src => src.CateguryOfUserId != null ? (int?)src.CateguryOfUserId.Value : null))
                .ForMember(dest => dest.BestStockId, opt => opt.MapFrom(src => src.BestStockId != null ? (int?)src.BestStockId.Value : null))
                .ForMember(dest => dest.BestStockUserCateguryId, opt => opt.MapFrom(src => src.BestStockUserCateguryId != null ? (int?)src.BestStockUserCateguryId.Value : null))
                .ForMember(dest => dest.DiscountDate, opt => opt.MapFrom(src => src.DiscountDate != null ? src.DiscountDate.ToDateTime() : (DateTime?)null))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDate.ToDateTime()));

            // Other Product-related mappings
            CreateMap<ProductParameterDto, ProductParameter>().ReverseMap();
            CreateMap<ProductSearchParameterDto, ProductSearchParameter>().ReverseMap();
            CreateMap<ProductListShowDto, ProductListShow>().ReverseMap();
            CreateMap<ProductDetailShowDto, ProductDetailShow>().ReverseMap();
            
            // Order DTO to Proto mapping
            CreateMap<OrderDto, Order>()
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => Timestamp.FromDateTime(DateTime.SpecifyKind(src.CreateDate, DateTimeKind.Utc))))
                .ForMember(dest => dest.IntroCode, opt => opt.MapFrom(src => src.IntroCode != null ? new StringValue { Value = src.IntroCode } : null))
                .ForMember(dest => dest.ConfirmPayment, opt => opt.MapFrom(src => src.ConfirmPayment.HasValue ? new BoolValue { Value = src.ConfirmPayment.Value } : null))
                .ForMember(dest => dest.FactorFile, opt => opt.MapFrom(src => src.FactorFile != null ? new StringValue { Value = src.FactorFile } : null));

            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDate.ToDateTime()))
                .ForMember(dest => dest.IntroCode, opt => opt.MapFrom(src => src.IntroCode != null ? src.IntroCode.Value : null))
                .ForMember(dest => dest.ConfirmPayment, opt => opt.MapFrom(src => src.ConfirmPayment != null ? (bool?)src.ConfirmPayment.Value : null))
                .ForMember(dest => dest.FactorFile, opt => opt.MapFrom(src => src.FactorFile != null ? src.FactorFile.Value : null));

            // User DTO to Proto mapping
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName != null ? new StringValue { Value = src.FirstName } : null))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName != null ? new StringValue { Value = src.LastName } : null))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName != null ? new StringValue { Value = src.FullName } : null))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId != null ? new StringValue { Value = src.UserId } : null))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Avatar != null ? new StringValue { Value = src.Avatar } : null))
                .ForMember(dest => dest.LastLoginDate, opt => opt.MapFrom(src => Timestamp.FromDateTime(DateTime.SpecifyKind(src.LastLoginDate, DateTimeKind.Utc))))
                .ForMember(dest => dest.IsDelete, opt => opt.MapFrom(src => src.IsDelete.HasValue ? new BoolValue { Value = src.IsDelete.Value } : null));

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName != null ? src.FirstName.Value : null))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName != null ? src.LastName.Value : null))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName != null ? src.FullName.Value : null))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId != null ? src.UserId.Value : null))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Avatar != null ? src.Avatar.Value : null))
                .ForMember(dest => dest.LastLoginDate, opt => opt.MapFrom(src => src.LastLoginDate.ToDateTime()))
                .ForMember(dest => dest.IsDelete, opt => opt.MapFrom(src => src.IsDelete != null ? (bool?)src.IsDelete.Value : null));

            // UserShipping DTO to Proto mapping
            CreateMap<UserShippingDto, UserShipping>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName != null ? new StringValue { Value = src.FirstName } : null))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName != null ? new StringValue { Value = src.LastName } : null))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone != null ? new StringValue { Value = src.Phone } : null))
                .ForMember(dest => dest.Mobile, opt => opt.MapFrom(src => src.Mobile != null ? new StringValue { Value = src.Mobile } : null))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country != null ? new StringValue { Value = src.Country } : null))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State != null ? new StringValue { Value = src.State } : null))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City != null ? new StringValue { Value = src.City } : null))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address != null ? new StringValue { Value = src.Address } : null))
                .ForMember(dest => dest.PostCode, opt => opt.MapFrom(src => src.PostCode != null ? new StringValue { Value = src.PostCode } : null));

            CreateMap<UserShipping, UserShippingDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName != null ? src.FirstName.Value : null))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName != null ? src.LastName.Value : null))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone != null ? src.Phone.Value : null))
                .ForMember(dest => dest.Mobile, opt => opt.MapFrom(src => src.Mobile != null ? src.Mobile.Value : null))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country != null ? src.Country.Value : null))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State != null ? src.State.Value : null))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City != null ? src.City.Value : null))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address != null ? src.Address.Value : null))
                .ForMember(dest => dest.PostCode, opt => opt.MapFrom(src => src.PostCode != null ? src.PostCode.Value : null));

            // Add mappings for all other DTOs as needed
        }
    }
}
