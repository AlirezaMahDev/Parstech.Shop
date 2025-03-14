using Grpc.Core;
using MediatR;
using Parstech.Shop.Shared.Protos.Product;
using Shop.Application.Features.Api.Requests.Queries;
using Shop.Application.Features.Product.Requests.Queries;

namespace Shop.ApiService.Services
{
    public class ProductGrpcService : ProductService.ProductServiceBase
    {
        private readonly IMediator _mediator;
        
        public ProductGrpcService(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public override async Task<ProductListResponse> GetProducts(ProductListRequest request, ServerCallContext context)
        {
            try
            {
                var parameter = new Shop.Application.DTOs.Paging.ParameterDto
                {
                    CurrentPage = request.Page,
                    TakePage = request.PageSize,
                    SearchKey = request.SearchTerm ?? string.Empty
                };
                
                var filter = new Shop.Application.DTOs.Product.ProductFilterDto
                {
                    CateguryId = request.CategoryId ?? 0,
                    BrandId = request.BrandId ?? 0,
                    StoreId = request.StoreId ?? 0,
                    OnlyAvailable = request.ShowOnlyAvailable ?? false,
                    OnlyDiscount = request.ShowOnlyDiscounted ?? false,
                    MinPrice = (long)(request.MinPrice ?? 0),
                    MaxPrice = (long)(request.MaxPrice ?? 0)
                };
                
                var products = await _mediator.Send(new ProductsListByPagingQueryReq(parameter, filter));
                
                var response = new ProductListResponse
                {
                    Page = products.CurrentPage,
                    PageSize = request.PageSize,
                    TotalCount = products.RowCount,
                    TotalPages = products.PageCount
                };
                
                foreach (var product in products.List)
                {
                    response.Products.Add(new ProductItem
                    {
                        Id = product.Id,
                        Name = product.Name ?? string.Empty,
                        ShortLink = product.ShortLink ?? string.Empty,
                        Image = product.Image ?? string.Empty,
                        Price = product.Price,
                        SalePrice = product.SalePrice,
                        DiscountPrice = product.DiscountPrice,
                        StockStatus = product.StockStatus,
                        BrandName = product.BrandName ?? string.Empty,
                        CategoryName = product.CateguryName ?? string.Empty
                    });
                }
                
                return response;
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }
        
        public override async Task<ProductResponse> GetProductById(ProductRequest request, ServerCallContext context)
        {
            try
            {
                var product = await _mediator.Send(new ProductByIdQueryReq(request.ProductId));
                
                return new ProductResponse
                {
                    Id = product.Id,
                    Name = product.Name ?? string.Empty,
                    LatinName = product.LatinName ?? string.Empty,
                    Code = product.Code ?? string.Empty,
                    Price = product.Price,
                    SalePrice = product.SalePrice,
                    DiscountPrice = product.DiscountPrice,
                    DiscountDate = product.DiscountDate?.ToString() ?? string.Empty,
                    BasePrice = product.BasePrice,
                    StockStatus = product.StockStatus,
                    Quantity = product.Quantity,
                    Description = product.Description ?? string.Empty,
                    ShortDescription = product.ShortDescription ?? string.Empty,
                    ShortLink = product.ShortLink ?? string.Empty,
                    TypeId = product.TypeId,
                    TypeName = product.TypeName ?? string.Empty,
                    StoreId = product.StoreId,
                    StoreName = product.StoreName ?? string.Empty,
                    Image = product.Image ?? string.Empty,
                    BrandId = product.BrandId,
                    BrandName = product.BrandName ?? string.Empty,
                    CategoryId = product.CateguryId,
                    CategoryName = product.CateguryName ?? string.Empty,
                    VisitCount = product.Visit,
                    SalesCount = product.CountSale,
                    IsActive = product.IsActive
                };
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }
        
        public override async Task<SitemapProductsResponse> GetProductsForSitemap(SitemapProductsRequest request, ServerCallContext context)
        {
            try
            {
                var products = await _mediator.Send(new SiteMapGenerateQueryReq());
                
                var response = new SitemapProductsResponse();
                foreach (var product in products)
                {
                    if (product.loc.StartsWith("/Products/Detail/"))
                    {
                        var parts = product.loc.Split('/');
                        if (parts.Length >= 4 && int.TryParse(parts[parts.Length - 1], out int productId))
                        {
                            response.Products.Add(new SitemapProduct
                            {
                                Id = productId,
                                ShortLink = parts[parts.Length - 2],
                                UpdatedAt = product.lastmod
                            });
                        }
                    }
                }
                
                return response;
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }
    }
} 