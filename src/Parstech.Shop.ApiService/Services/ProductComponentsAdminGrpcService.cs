using Grpc.Core;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Categury.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.Product.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.ProductCategury.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.ProductCategury.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.ProductGallery.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.ProductGallery.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.ProductProperty.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Services;

public class ProductComponentsAdminGrpcService : ProductComponentsAdminService.ProductComponentsAdminServiceBase
{
    private readonly IMediator _mediator;
    private readonly IProductStockPriceRepository _productStockPriceRepository;

    public ProductComponentsAdminGrpcService(
        IMediator mediator,
        IProductStockPriceRepository productStockPriceRepository)
    {
        _mediator = mediator;
        _productStockPriceRepository = productStockPriceRepository;
    }

    #region Gallery Operations

    public override async Task<ProductGalleryListResponse> GetGalleriesOfProduct(ProductIdRequest request,
        ServerCallContext context)
    {
        try
        {
            var galleries = await _mediator.Send(new GalleriesOfProductQueryReq(request.ProductId));

            var response = new ProductGalleryListResponse { IsSuccess = true };

            foreach (var gallery in galleries)
            {
                response.Galleries.Add(new ProductGalleryDto
                {
                    Id = gallery.Id,
                    ProductId = gallery.ProductId,
                    ImageName = gallery.ImageName ?? string.Empty,
                    IsMain = gallery.IsMain
                });
            }

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    public override async Task<ResponseDto> CreateProductGallery(ProductGalleryRequest request,
        ServerCallContext context)
    {
        try
        {
            var galleryDto = new Shop.Application.DTOs.ProductGallery.ProductGalleryDto
            {
                Id = request.Gallery.Id,
                ProductId = request.Gallery.ProductId,
                ImageName = request.Gallery.ImageName,
                IsMain = request.Gallery.IsMain
            };

            var result = await _mediator.Send(new ProductGalleryCreateCommandReq(galleryDto));

            // Handle user role check and product update
            if (context.GetHttpContext().User.IsInRole("Store"))
            {
                var product = await _mediator.Send(new ProductReadCommandReq(request.Gallery.ProductId));
                product.IsActive = false;
                await _mediator.Send(new ProductUpdateCommandReq(product));
            }

            return new() { IsSuccessed = result.IsSuccessed, Message = result.Message ?? string.Empty };
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    public override async Task<ResponseDto> DeleteProductGallery(GalleryIdRequest request, ServerCallContext context)
    {
        try
        {
            await _mediator.Send(new ProductGalleryDeleteCommandReq(request.GalleryId));

            // Handle user role check and product update
            if (context.GetHttpContext().User.IsInRole("Store"))
            {
                var product = await _mediator.Send(new ProductReadCommandReq(request.ProductId));
                product.IsActive = false;
                await _mediator.Send(new ProductUpdateCommandReq(product));
            }

            return new() { IsSuccessed = true, Message = "تصویر محصول با موفقیت حذف گردید." };
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    public override async Task<ResponseDto> ChangeMainGallery(ChangeMainGalleryRequest request,
        ServerCallContext context)
    {
        try
        {
            var result = await _mediator.Send(new ChangeMainGalleryCommandReq(request.GalleryId, request.ProductId));

            return new() { IsSuccessed = result.IsSuccessed, Message = result.Message ?? string.Empty };
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    #endregion

    #region Category Operations

    public override async Task<ProductCategoryListResponse> GetCategoriesOfProduct(ProductIdRequest request,
        ServerCallContext context)
    {
        try
        {
            var categories = await _mediator.Send(new CateguriesOfProductQueryReq(request.ProductId));

            var response = new ProductCategoryListResponse { IsSuccess = true };

            foreach (var category in categories)
            {
                response.Categories.Add(new ProductCategoryDto
                {
                    Id = category.Id,
                    ProductId = category.ProductId,
                    CategoryId = category.CateguryId,
                    CategoryTitle = category.CateguryTitle ?? string.Empty,
                    CategoryLatinTitle = category.CateguryLatinTitle ?? string.Empty
                });
            }

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    public override async Task<ProductCategoryResponse> GetCategoryOfProduct(ProductIdRequest request,
        ServerCallContext context)
    {
        try
        {
            var category = await _mediator.Send(new CateguryOfProductQueryReq(request.ProductId));

            var response = new ProductCategoryResponse
            {
                IsSuccess = true,
                Category = new ProductCategoryDto
                {
                    Id = category.Id,
                    ProductId = category.ProductId,
                    CategoryId = category.CateguryId,
                    CategoryTitle = category.CateguryTitle ?? string.Empty,
                    CategoryLatinTitle = category.CateguryLatinTitle ?? string.Empty
                }
            };

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    public override async Task<CategoryListResponse> GetAllCategories(FilterRequest request, ServerCallContext context)
    {
        try
        {
            var categories = await _mediator.Send(new CateguryReadCommandReq(request.Filter));

            var response = new CategoryListResponse { IsSuccess = true };

            foreach (var category in categories)
            {
                response.Categories.Add(new CategoryDto
                {
                    Id = category.Id,
                    Title = category.Title ?? string.Empty,
                    LatinTitle = category.LatinTitle ?? string.Empty,
                    ParentId = category.ParentId,
                    IsParent = category.IsParnet,
                    Image = category.Image ?? string.Empty
                });
            }

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    public override async Task<ProductIdResponse> DeleteProductCategory(ProductIdRequest request,
        ServerCallContext context)
    {
        try
        {
            var productId = await _mediator.Send(new ProductCateguryDeleteCommandReq(request.ProductId));

            return new ProductIdResponse { ProductId = productId, IsSuccess = true };
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    #endregion

    #region Childs and Stock Operations

    public override async Task<ChildsAndStocksResponse> GetChildsAndProductStocks(ChildsAndStocksRequest request,
        ServerCallContext context)
    {
        try
        {
            var result =
                await _mediator.Send(new GetChildsAndProductStocksQueryReq(request.ProductId, request.StoreId));

            var response = new ChildsAndStocksResponse { IsSuccess = true };

            // Map child products
            if (result.Childs != null)
            {
                foreach (var child in result.Childs)
                {
                    response.Childs.Add(MapProductDto(child));
                }
            }

            // Map stock prices
            if (result.Stocks != null)
            {
                foreach (var stock in result.Stocks)
                {
                    response.Stocks.Add(new ProductStockPriceDto
                    {
                        Id = stock.Id,
                        ProductId = stock.ProductId,
                        StoreId = stock.StoreId,
                        StoreName = stock.StoreName ?? string.Empty,
                        RepId = stock.RepId,
                        RepName = stock.RepName ?? string.Empty,
                        Quantity = stock.Quantity,
                        Price = stock.Price,
                        SalePrice = stock.SalePrice,
                        SpecialPrice = stock.SpecialPrice,
                        ExpireDate = stock.ExpireDate ?? string.Empty,
                        StockNumber = stock.StockNumber ?? string.Empty,
                        SpecialDate = stock.SpecialDate ?? string.Empty
                    });
                }
            }

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    #endregion

    #region Feature Operations

    public override async Task<ProductPropertyListResponse> GetPropertiesOfProduct(ProductIdRequest request,
        ServerCallContext context)
    {
        try
        {
            var properties = await _mediator.Send(new PropertiesOfProductQueryReq(request.ProductId));

            var response = new ProductPropertyListResponse { IsSuccess = true };

            foreach (var property in properties)
            {
                response.Properties.Add(new ProductPropertyDto
                {
                    Id = property.Id,
                    ProductId = property.ProductId,
                    PropertyId = property.PropertyId,
                    PropertyValue = property.Value ?? string.Empty,
                    PropertyTitle = property.Title ?? string.Empty
                });
            }

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    #endregion

    #region Product Detail Operations

    public override async Task<ProductDetailResponse> GetProductDetail(ProductDetailRequest request,
        ServerCallContext context)
    {
        try
        {
            var productDetail =
                await _mediator.Send(
                    new ProductDetailShowQueryReq(request.ShortLink, request.StoreId, request.UserName));

            var response = new ProductDetailResponse
            {
                IsSuccess = true, Product = MapProductDto(productDetail.Product)
            };

            // Map galleries
            if (productDetail.Galleries != null)
            {
                foreach (var gallery in productDetail.Galleries)
                {
                    response.Galleries.Add(new ProductGalleryDto
                    {
                        Id = gallery.Id,
                        ProductId = gallery.ProductId,
                        ImageName = gallery.ImageName ?? string.Empty,
                        IsMain = gallery.IsMain
                    });
                }
            }

            // Map properties
            if (productDetail.Properties != null)
            {
                foreach (var property in productDetail.Properties)
                {
                    response.Properties.Add(new ProductPropertyDto
                    {
                        Id = property.Id,
                        ProductId = property.ProductId,
                        PropertyId = property.PropertyId,
                        PropertyValue = property.Value ?? string.Empty,
                        PropertyTitle = property.Title ?? string.Empty
                    });
                }
            }

            // Map categories
            if (productDetail.Categories != null)
            {
                foreach (var category in productDetail.Categories)
                {
                    response.Categories.Add(new ProductCategoryDto
                    {
                        Id = category.Id,
                        ProductId = category.ProductId,
                        CategoryId = category.CateguryId,
                        CategoryTitle = category.CateguryTitle ?? string.Empty,
                        CategoryLatinTitle = category.CateguryLatinTitle ?? string.Empty
                    });
                }
            }

            // Map stocks
            if (productDetail.Stocks != null)
            {
                foreach (var stock in productDetail.Stocks)
                {
                    response.Stocks.Add(new ProductStockPriceDto
                    {
                        Id = stock.Id,
                        ProductId = stock.ProductId,
                        StoreId = stock.StoreId,
                        StoreName = stock.StoreName ?? string.Empty,
                        RepId = stock.RepId,
                        RepName = stock.RepName ?? string.Empty,
                        Quantity = stock.Quantity,
                        Price = stock.Price,
                        SalePrice = stock.SalePrice,
                        SpecialPrice = stock.SpecialPrice,
                        ExpireDate = stock.ExpireDate ?? string.Empty,
                        StockNumber = stock.StockNumber ?? string.Empty,
                        SpecialDate = stock.SpecialDate ?? string.Empty
                    });
                }
            }

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    #endregion

    #region Helper Methods

    private ProductDto MapProductDto(Shop.Application.DTOs.Product.ProductDto product)
    {
        if (product == null)
        {
            return null;
        }

        return new()
        {
            Id = product.Id,
            Name = product.Name ?? string.Empty,
            LatinName = product.LatinName ?? string.Empty,
            ShortLink = product.ShortLink ?? string.Empty,
            Image = product.Image ?? string.Empty,
            Price = product.Price,
            SalePrice = product.SalePrice,
            TypeId = product.TypeId,
            IsActive = product.IsActive,
            IsSpecial = product.IsSpecial,
            WeightAndDimensions = product.WeightAndDimensions,
            Weight = product.Weight,
            Length = product.Length,
            Width = product.Width,
            Height = product.Height,
            Description = product.Description ?? string.Empty,
            Summary = product.Summary ?? string.Empty,
            IsStockManaged = product.IsStockManaged,
            IsUsedForShipping = product.IsUsedForShipping,
            FreeShipping = product.FreeShipping,
            IsFreeShipping = product.IsFreeShipping,
            StoreId = product.StoreId,
            StoreName = product.StoreName ?? string.Empty,
            ParentProductId = product.ParentProductId,
            TaxId = product.TaxId,
            TaxTitle = product.TaxTitle ?? string.Empty,
            TaxPercent = product.TaxPercent,
            BrandId = product.BrandId,
            BrandName = product.BrandName ?? string.Empty,
            WithTracking = product.WithTracking
        };
    }

    #endregion
}