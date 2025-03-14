using Grpc.Core;
using MediatR;
using Parstech.Shop.Shared.Protos.ProductDetail;
using Shop.Application.Features.Product.Requests.Queries;

namespace Shop.ApiService.Services
{
    public class ProductDetailGrpcService : ProductDetailService.ProductDetailServiceBase
    {
        private readonly IMediator _mediator;
        
        public ProductDetailGrpcService(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public override async Task<ProductDetailResponse> GetProductByShortLink(ProductDetailRequest request, ServerCallContext context)
        {
            try
            {
                var productDetail = await _mediator.Send(new ProductDetailPageByShortLinkQueryReq(
                    request.ShortLink, 
                    request.StoreId, 
                    request.UserName));
                
                var response = new ProductDetailResponse
                {
                    Id = productDetail.Id,
                    Name = productDetail.Name ?? string.Empty,
                    LatinName = productDetail.LatinName ?? string.Empty,
                    Code = productDetail.Code ?? string.Empty,
                    Price = productDetail.Price,
                    SalePrice = productDetail.SalePrice,
                    DiscountPrice = productDetail.DiscountPrice,
                    DiscountDate = productDetail.DiscountDate ?? string.Empty,
                    BasePrice = productDetail.BasePrice,
                    StockStatus = productDetail.StockStatus,
                    Quantity = productDetail.Quantity,
                    MaximumSaleInOrder = productDetail.MaximumSaleInOrder,
                    Score = productDetail.Score,
                    Description = productDetail.Description ?? string.Empty,
                    ShortDescription = productDetail.ShortDescription ?? string.Empty,
                    ShortLink = productDetail.ShortLink ?? string.Empty,
                    TypeId = productDetail.TypeId,
                    TypeName = productDetail.TypeName ?? string.Empty,
                    VariationName = productDetail.VariationName ?? string.Empty,
                    StoreId = productDetail.StoreId,
                    StoreName = productDetail.StoreName ?? string.Empty,
                    LatinStoreName = productDetail.LatinStoreName ?? string.Empty,
                    Image = productDetail.Image ?? string.Empty,
                    ParentId = productDetail.ParentId,
                    ParentProductName = productDetail.ParentProductName ?? string.Empty,
                    BrandId = productDetail.BrandId,
                    BrandName = productDetail.BrandName ?? string.Empty,
                    LatinBrandName = productDetail.LatinBrandName ?? string.Empty,
                    TaxId = productDetail.TaxId,
                    RepName = productDetail.RepName ?? string.Empty,
                    CreateDate = productDetail.CreateDate ?? string.Empty,
                    CateguryName = productDetail.CateguryName ?? string.Empty,
                    CateguryLatinName = productDetail.CateguryLatinName ?? string.Empty,
                    SingleSale = productDetail.SingleSale,
                    QuantityPerBundle = productDetail.QuantityPerBundle
                };
                
                if (productDetail.IsInFavorites.HasValue)
                {
                    response.IsInFavorites = productDetail.IsInFavorites.Value;
                }
                
                if (productDetail.IsInCompare.HasValue)
                {
                    response.IsInCompare = productDetail.IsInCompare.Value;
                }
                
                if (productDetail.Properties != null)
                {
                    foreach (var property in productDetail.Properties)
                    {
                        response.Properties.Add(new PropertyDetail
                        {
                            Id = property.Id,
                            Name = property.Name ?? string.Empty,
                            Value = property.Value ?? string.Empty
                        });
                    }
                }
                
                if (productDetail.RelatedProducts != null)
                {
                    foreach (var related in productDetail.RelatedProducts)
                    {
                        response.RelatedProducts.Add(new RelatedProduct
                        {
                            Id = related.Id,
                            Name = related.Name ?? string.Empty,
                            Image = related.Image ?? string.Empty,
                            Price = related.Price,
                            SalePrice = related.SalePrice,
                            ShortLink = related.ShortLink ?? string.Empty
                        });
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