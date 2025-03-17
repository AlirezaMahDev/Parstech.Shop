using Grpc.Core;

using MediatR;

using Parstech.Shop.ApiService.Application.Features.Categury.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.Section.Requests.Queries;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Services;

public class SectionGrpcService : SectionService.SectionServiceBase
{
    private readonly IMediator _mediator;

    public SectionGrpcService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<SectionResponse> GetSections(SectionRequest request, ServerCallContext context)
    {
        try
        {
            var sections = await _mediator.Send(new SectionsQueryReq(request.ParentId));

            var response = new SectionResponse();
            foreach (var section in sections)
            {
                var protoSection = new Section
                {
                    Id = section.Id,
                    Name = section.Name ?? string.Empty,
                    Description = section.Description ?? string.Empty,
                    Image = section.Image ?? string.Empty,
                    ParentId = section.ParentId,
                    IsActive = section.IsActive
                };

                if (section.SectionDetails != null)
                {
                    foreach (var detail in section.SectionDetails)
                    {
                        protoSection.Details.Add(new SectionDetail
                        {
                            Id = detail.Id,
                            SectionId = detail.SectionId,
                            Title = detail.Title ?? string.Empty,
                            Description = detail.Description ?? string.Empty,
                            Image = detail.Image ?? string.Empty,
                            Link = detail.Link ?? string.Empty,
                            IsActive = detail.IsActive,
                            Order = detail.Order
                        });
                    }
                }

                response.Sections.Add(protoSection);
            }

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<SectionDetailsResponse> GetSectionDetails(SectionDetailsRequest request,
        ServerCallContext context)
    {
        try
        {
            var details = await _mediator.Send(new SectionDetailsQueryReq(request.SectionId));

            var response = new SectionDetailsResponse();
            foreach (var detail in details)
            {
                response.Details.Add(new SectionDetail
                {
                    Id = detail.Id,
                    SectionId = detail.SectionId,
                    Title = detail.Title ?? string.Empty,
                    Description = detail.Description ?? string.Empty,
                    Image = detail.Image ?? string.Empty,
                    Link = detail.Link ?? string.Empty,
                    IsActive = detail.IsActive,
                    Order = detail.Order
                });
            }

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<SectionWithDetailsResponse> GetSectionAndDetailsById(SectionByIdRequest request,
        ServerCallContext context)
    {
        try
        {
            var section = await _mediator.Send(new SectionAndDetailsReadByIdQueryReq(request.SectionId));

            var response = new SectionWithDetailsResponse
            {
                Id = section.Id,
                Name = section.Name ?? string.Empty,
                Description = section.Description ?? string.Empty,
                Image = section.Image ?? string.Empty,
                ParentId = section.ParentId,
                SectionTypeId = section.SectionTypeId,
                CategoryId = section.CateguryId,
                ProductId = section.ProductId,
                LatinCategoryName = section.latinCateguryName ?? string.Empty,
                IsActive = section.IsActive
            };

            // Add section details
            if (section.SectionDetails != null)
            {
                foreach (var detail in section.SectionDetails)
                {
                    response.SectionDetails.Add(new SectionDetail
                    {
                        Id = detail.Id,
                        SectionId = detail.SectionId,
                        Title = detail.Title ?? string.Empty,
                        Description = detail.Description ?? string.Empty,
                        Image = detail.Image ?? string.Empty,
                        Link = detail.Link ?? string.Empty,
                        IsActive = detail.IsActive,
                        Order = detail.Order
                    });
                }
            }

            // Add products if available
            if (section.ProductCateguries != null)
            {
                foreach (var product in section.ProductCateguries)
                {
                    response.Products.Add(new ProductItem
                    {
                        Id = product.Id,
                        Name = product.ProductName ?? string.Empty,
                        LatinName = product.ProductLatinName ?? string.Empty,
                        Description = product.Description ?? string.Empty,
                        Image = product.MainImage ?? string.Empty,
                        Price = product.Price,
                        DiscountedPrice = product.DiscountedPrice,
                        DiscountPercent = product.DiscountPercent,
                        HasDiscount = product.HasDiscount,
                        CategoryId = product.CateguryId,
                        CategoryName = product.CateguryName ?? string.Empty,
                        CategoryLatinName = product.CateguryLatinName ?? string.Empty,
                        IsAvailable = product.IsAvailable,
                        BrandId = product.BrandId,
                        BrandName = product.BrandName ?? string.Empty,
                        BrandLatinName = product.BrandLatinName ?? string.Empty,
                        IsFavorite = product.IsFavorite,
                        InComparison = product.InComparison
                    });
                }
            }

            // Add product if available
            if (section.Product != null)
            {
                response.Product = new ProductItem
                {
                    Id = section.Product.Id,
                    Name = section.Product.Name ?? string.Empty,
                    LatinName = section.Product.LatinName ?? string.Empty,
                    Description = section.Product.Description ?? string.Empty,
                    Image = section.Product.MainImage ?? string.Empty,
                    Price = section.Product.Price,
                    DiscountedPrice = section.Product.DiscountedPrice ?? 0,
                    DiscountPercent = section.Product.DiscountPercent ?? 0,
                    HasDiscount = section.Product.DiscountedPrice.HasValue && section.Product.DiscountedPrice > 0,
                    CategoryId = section.Product.CateguryId,
                    CategoryName = string.Empty, // Not available in the DTO
                    CategoryLatinName = string.Empty, // Not available in the DTO
                    IsAvailable = section.Product.IsAvailable,
                    BrandId = section.Product.BrandId,
                    BrandName = section.Product.BrandName ?? string.Empty,
                    BrandLatinName = string.Empty // Not available in the DTO
                };
            }

            // Add brands if available
            if (section.Brands != null)
            {
                foreach (var brand in section.Brands)
                {
                    response.Brands.Add(new BrandItem
                    {
                        Id = brand.Id,
                        Name = brand.Name ?? string.Empty,
                        LatinName = brand.LatinName ?? string.Empty,
                        Logo = brand.Logo ?? string.Empty,
                        IsActive = brand.IsActive
                    });
                }
            }

            // If we need to fetch category information
            if (section.SectionTypeId == 2 &&
                section.CateguryId != 0 &&
                string.IsNullOrEmpty(section.latinCateguryName))
            {
                var category = await _mediator.Send(new CateguryReadByIdQueryReq(section.CateguryId));
                response.LatinCategoryName = category.LatinGroupTitle ?? string.Empty;
            }

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<SectionWithDetailsResponse> GetSectionAndDetailsByStore(SectionByStoreRequest request,
        ServerCallContext context)
    {
        try
        {
            var section = await _mediator.Send(new SectionAndDetailsReadStoreQueryReq(request.Store));

            var response = new SectionWithDetailsResponse
            {
                Id = section.Id,
                Name = section.Name ?? string.Empty,
                Description = section.Description ?? string.Empty,
                Image = section.Image ?? string.Empty,
                ParentId = section.ParentId,
                SectionTypeId = section.SectionTypeId,
                CategoryId = section.CateguryId,
                ProductId = section.ProductId,
                LatinCategoryName = section.latinCateguryName ?? string.Empty,
                IsActive = section.IsActive
            };

            // Add section details
            if (section.SectionDetails != null)
            {
                foreach (var detail in section.SectionDetails)
                {
                    response.SectionDetails.Add(new SectionDetail
                    {
                        Id = detail.Id,
                        SectionId = detail.SectionId,
                        Title = detail.Title ?? string.Empty,
                        Description = detail.Description ?? string.Empty,
                        Image = detail.Image ?? string.Empty,
                        Link = detail.Link ?? string.Empty,
                        IsActive = detail.IsActive,
                        Order = detail.Order,
                        ResponsiveSize = detail.ResponsiveSize ?? string.Empty,
                        Caption = detail.Caption ?? string.Empty,
                        SubCaption = detail.SubCaption ?? string.Empty,
                        BackgroundImage = detail.BackgroundImage ?? string.Empty,
                        SlideNavName = detail.SlideNavName ?? string.Empty,
                        Alt = detail.Alt ?? string.Empty
                    });
                }
            }

            // Add products if available
            if (section.ProductCateguries != null)
            {
                foreach (var product in section.ProductCateguries)
                {
                    response.Products.Add(new ProductItem
                    {
                        Id = product.Id,
                        Name = product.ProductName ?? string.Empty,
                        LatinName = product.ProductLatinName ?? string.Empty,
                        Description = product.Description ?? string.Empty,
                        Image = product.MainImage ?? string.Empty,
                        Price = product.Price,
                        DiscountedPrice = product.DiscountedPrice,
                        DiscountPercent = product.DiscountPercent,
                        HasDiscount = product.HasDiscount,
                        CategoryId = product.CateguryId,
                        CategoryName = product.CateguryName ?? string.Empty,
                        CategoryLatinName = product.CateguryLatinName ?? string.Empty,
                        IsAvailable = product.IsAvailable,
                        BrandId = product.BrandId,
                        BrandName = product.BrandName ?? string.Empty,
                        BrandLatinName = product.BrandLatinName ?? string.Empty,
                        IsFavorite = product.IsFavorite,
                        InComparison = product.InComparison,
                        ShortLink = product.ShortLink ?? string.Empty,
                        ProductStockPriceId = product.ProductStockPriceId,
                        DiscountDate = product.DiscountDate ?? string.Empty,
                        Quantity = product.Quantity,
                        SalePrice = product.SalePrice,
                        DiscountPrice = product.DiscountPrice,
                        ProductId = product.ProductId
                    });
                }
            }

            // Add product if available
            if (section.Product != null)
            {
                response.Product = new ProductItem
                {
                    Id = section.Product.Id,
                    Name = section.Product.Name ?? string.Empty,
                    LatinName = section.Product.LatinName ?? string.Empty,
                    Description = section.Product.Description ?? string.Empty,
                    Image = section.Product.MainImage ?? string.Empty,
                    Price = section.Product.Price,
                    DiscountedPrice = section.Product.DiscountedPrice ?? 0,
                    DiscountPercent = section.Product.DiscountPercent ?? 0,
                    HasDiscount = section.Product.DiscountedPrice.HasValue && section.Product.DiscountedPrice > 0,
                    CategoryId = section.Product.CateguryId,
                    CategoryName = string.Empty, // Not available in the DTO
                    CategoryLatinName = string.Empty, // Not available in the DTO
                    IsAvailable = section.Product.IsAvailable,
                    BrandId = section.Product.BrandId,
                    BrandName = section.Product.BrandName ?? string.Empty,
                    BrandLatinName = string.Empty // Not available in the DTO
                };
            }

            // Add brands if available
            if (section.Brands != null)
            {
                foreach (var brand in section.Brands)
                {
                    response.Brands.Add(new BrandItem
                    {
                        Id = brand.Id,
                        Name = brand.Name ?? string.Empty,
                        LatinName = brand.LatinName ?? string.Empty,
                        Logo = brand.Logo ?? string.Empty,
                        IsActive = brand.IsActive
                    });
                }
            }

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }
}