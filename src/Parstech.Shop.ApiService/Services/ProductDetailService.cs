using Grpc.Core;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;

namespace Parstech.Shop.ApiService.Services;

public class ProductDetailService : ProductDetailServiceBase
{
    private readonly IMediator _mediator;
    private readonly IProductRepository _productRepository;

    public ProductDetailService(IMediator mediator, IProductRepository productRepository)
    {
        _mediator = mediator;
        _productRepository = productRepository;
    }

    public override async Task<ProductDetailResponse> GetProductByShortLink(ProductDetailRequest request,
        ServerCallContext context)
    {
        try
        {
            var product =
                await _mediator.Send(
                    new ProductDetailShowQueryReq(request.ShortLink, request.StoreId, request.UserName));

            var response = new ProductDetailResponse
            {
                Id = product.Id,
                Name = product.Name,
                LatinName = product.LatinName,
                Code = product.Code,
                Price = product.Price,
                SalePrice = product.SalePrice,
                DiscountPrice = product.DiscountPrice,
                DiscountDate = product.DiscountDate.ToString(),
                BasePrice = product.BasePrice,
                StockStatus = product.StockStatus,
                Quantity = product.Quantity,
                MaximumSaleInOrder = product.MaximumSaleInOrder,
                Score = product.Score,
                Description = product.Description,
                ShortDescription = product.ShortDescription,
                ShortLink = product.ShortLink,
                TypeId = product.TypeId,
                TypeName = product.TypeName,
                VariationName = product.VariationName,
                StoreId = product.StoreId,
                StoreName = product.StoreName,
                LatinStoreName = product.LatinStoreName,
                Image = product.Image,
                ParentId = product.ParentId,
                ParentProductName = product.ParentProductName,
                BrandId = product.BrandId,
                BrandName = product.BrandName,
                LatinBrandName = product.LatinBrandName,
                TaxId = product.TaxId,
                RepName = product.RepName,
                CreateDate = product.CreateDate?.ToString(),
                CateguryName = product.CateguryName,
                CateguryLatinName = product.CateguryLatinName,
                SingleSale = product.SingleSale,
                QuantityPerBundle = product.QuantityPerBundle
            };

            // Add properties
            if (product.Properties != null)
            {
                foreach (var prop in product.Properties)
                {
                    response.Properties.Add(new PropertyDetail { Id = prop.Id, Name = prop.Name, Value = prop.Value });
                }
            }

            // Add related products
            if (product.RelatedProducts != null)
            {
                foreach (var related in product.RelatedProducts)
                {
                    response.RelatedProducts.Add(new RelatedProduct
                    {
                        Id = related.Id,
                        Name = related.Name,
                        Image = related.Image,
                        Price = related.Price,
                        SalePrice = related.SalePrice,
                        ShortLink = related.ShortLink
                    });
                }
            }

            // Add favorites and compare flags
            if (!string.IsNullOrEmpty(request.UserName))
            {
                response.IsInFavorites = product.IsInFavorites;
                response.IsInCompare = product.IsInCompare;
            }

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }
}