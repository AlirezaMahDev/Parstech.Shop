using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.Web.Services.GrpcClients;

namespace Parstech.Shop.Web.Pages.Products;

public class DetailModel : PageModel
{
    #region Constructor

    private readonly ProductDetailGrpcClient _productDetailClient;
    private readonly ProductGalleryGrpcClient _galleryClient;
    private readonly TorobGrpcClient _torobClient;

    public DetailModel(
        ProductDetailGrpcClient productDetailClient,
        ProductGalleryGrpcClient galleryClient,
        TorobGrpcClient torobClient)
    {
        _productDetailClient = productDetailClient;
        _galleryClient = galleryClient;
        _torobClient = torobClient;
    }

    #endregion

    #region Properties

    [BindProperty]
    public ResponseDto Response { get; set; } = new ResponseDto();

    [BindProperty]
    public ProductDetailShowDto Item { get; set; } = new ProductDetailShowDto();

    [BindProperty]
    public string ShortLink { get; set; }

    [BindProperty]
    public int StoreId { get; set; }

    [BindProperty]
    public List<ProductGalleryDto> Galleries { get; set; }

    public TorobDto Torob { get; set; }

    #endregion

    #region Get

    public async Task<IActionResult> OnGet(string shortLink, int storeId)
    {
        try
        {
            ShortLink = shortLink;
            StoreId = storeId;

            // Get user name if authenticated
            string userName = User.Identity.IsAuthenticated ? User.Identity.Name : null;

            // Get product detail
            var productDetail = await _productDetailClient.GetProductByShortLinkAsync(shortLink, storeId, userName);

            // Map to DTO
            Item = new ProductDetailShowDto
            {
                Id = productDetail.Id,
                Name = productDetail.Name,
                LatinName = productDetail.LatinName,
                Code = productDetail.Code,
                Price = productDetail.Price,
                SalePrice = productDetail.SalePrice,
                DiscountPrice = productDetail.DiscountPrice,
                DiscountDate = DateTime.Parse(productDetail.DiscountDate),
                BasePrice = productDetail.BasePrice,
                StockStatus = productDetail.StockStatus,
                Quantity = productDetail.Quantity,
                MaximumSaleInOrder = productDetail.MaximumSaleInOrder,
                Score = productDetail.Score,
                Description = productDetail.Description,
                ShortDescription = productDetail.ShortDescription,
                ShortLink = productDetail.ShortLink,
                TypeId = productDetail.TypeId,
                TypeName = productDetail.TypeName,
                VariationName = productDetail.VariationName,
                StoreId = productDetail.StoreId,
                StoreName = productDetail.StoreName,
                LatinStoreName = productDetail.LatinStoreName,
                Image = productDetail.Image,
                ParentId = productDetail.ParentId,
                ParentProductName = productDetail.ParentProductName,
                BrandId = productDetail.BrandId,
                BrandName = productDetail.BrandName,
                LatinBrandName = productDetail.LatinBrandName,
                TaxId = productDetail.TaxId,
                RepName = productDetail.RepName,
                CreateDate = DateTime.Parse(productDetail.CreateDate),
                CateguryName = productDetail.CateguryName,
                CateguryLatinName = productDetail.CateguryLatinName,
                SingleSale = productDetail.SingleSale,
                QuantityPerBundle = productDetail.QuantityPerBundle,
                IsInFavorites = productDetail.IsInFavorites,
                IsInCompare = productDetail.IsInCompare,
                Properties =
                    productDetail.Properties
                        .Select(p => new PropertyDetailDto { Id = p.Id, Name = p.Name, Value = p.Value })
                        .ToList(),
                RelatedProducts = productDetail.RelatedProducts.Select(r => new RelatedProductDto
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Image = r.Image,
                        Price = r.Price,
                        SalePrice = r.SalePrice,
                        ShortLink = r.ShortLink
                    })
                    .ToList()
            };

            // Get product galleries
            var galleryResponse = await _galleryClient.GetProductGalleriesAsync(Item.Id);
            Galleries = galleryResponse.Galleries.Select(g => new ProductGalleryDto
                {
                    Id = g.Id,
                    ProductId = g.ProductId,
                    ImageName = g.ImageName,
                    Alt = g.Alt,
                    IsMain = g.IsMain
                })
                .ToList();

            // Get Torob data
            string baseUrl = $"{Request.Scheme}://{Request.Host.ToUriComponent()}";
            var torobData = await _torobClient.GetTorobProductAsync(storeId, baseUrl);
            Torob = new TorobDto
            {
                ProductId = torobData.ProductId,
                PageUrl = torobData.PageUrl,
                Price = torobData.Price,
                Availability = torobData.Availability,
                OldPrice = torobData.OldPrice,
                Image = torobData.Image,
                Content = torobData.Content,
                Name = torobData.Name
            };

            return Page();
        }
        catch (Exception ex)
        {
            Response.IsSuccessed = false;
            Response.Message = $"Error loading product details: {ex.Message}";
            return Page();
        }
    }

    public async Task<IActionResult> OnPostData()
    {
        try
        {
            // Get user name if authenticated
            string userName = User.Identity.IsAuthenticated ? User.Identity.Name : null;

            // Get product detail
            var productDetail = await _productDetailClient.GetProductByShortLinkAsync(ShortLink, StoreId, userName);

            // Map to DTO
            var item = new ProductDetailShowDto
            {
                Id = productDetail.Id,
                Name = productDetail.Name,
                LatinName = productDetail.LatinName,
                Code = productDetail.Code,
                Price = productDetail.Price,
                SalePrice = productDetail.SalePrice,
                DiscountPrice = productDetail.DiscountPrice,
                DiscountDate = DateTime.Parse(productDetail.DiscountDate),
                BasePrice = productDetail.BasePrice,
                StockStatus = productDetail.StockStatus,
                Quantity = productDetail.Quantity,
                MaximumSaleInOrder = productDetail.MaximumSaleInOrder,
                Score = productDetail.Score,
                Description = productDetail.Description,
                ShortDescription = productDetail.ShortDescription,
                ShortLink = productDetail.ShortLink,
                TypeId = productDetail.TypeId,
                TypeName = productDetail.TypeName,
                VariationName = productDetail.VariationName,
                StoreId = productDetail.StoreId,
                StoreName = productDetail.StoreName,
                LatinStoreName = productDetail.LatinStoreName,
                Image = productDetail.Image,
                ParentId = productDetail.ParentId,
                ParentProductName = productDetail.ParentProductName,
                BrandId = productDetail.BrandId,
                BrandName = productDetail.BrandName,
                LatinBrandName = productDetail.LatinBrandName,
                TaxId = productDetail.TaxId,
                RepName = productDetail.RepName,
                CreateDate = DateTime.Parse(productDetail.CreateDate),
                CateguryName = productDetail.CateguryName,
                CateguryLatinName = productDetail.CateguryLatinName,
                SingleSale = productDetail.SingleSale,
                QuantityPerBundle = productDetail.QuantityPerBundle,
                IsInFavorites = productDetail.IsInFavorites,
                IsInCompare = productDetail.IsInCompare,
                Properties =
                    productDetail.Properties
                        .Select(p => new PropertyDetailDto { Id = p.Id, Name = p.Name, Value = p.Value })
                        .ToList(),
                RelatedProducts = productDetail.RelatedProducts.Select(r => new RelatedProductDto
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Image = r.Image,
                        Price = r.Price,
                        SalePrice = r.SalePrice,
                        ShortLink = r.ShortLink
                    })
                    .ToList()
            };

            Response.Object = item;
            Response.IsSuccessed = true;
            return new JsonResult(Response);
        }
        catch (Exception ex)
        {
            Response.IsSuccessed = false;
            Response.Message = $"Error loading product details: {ex.Message}";
            return new JsonResult(Response);
        }
    }

    #endregion
}