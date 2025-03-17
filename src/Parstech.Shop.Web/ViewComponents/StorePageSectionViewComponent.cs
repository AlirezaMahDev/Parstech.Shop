using Microsoft.AspNetCore.Mvc;

using Parstech.Shop.Web.Services.GrpcClients;

using System.Security.Claims;

namespace Parstech.Shop.Web.ViewComponents;

[ViewComponent(Name = "StorePageSection")]
public class StorePageSectionViewComponent : ViewComponent
{
    private readonly SectionGrpcClient _sectionClient;
    private readonly ProductGrpcClient _productClient;
    private readonly BrandGrpcClient _brandClient;

    public StorePageSectionViewComponent(
        SectionGrpcClient sectionClient,
        ProductGrpcClient productClient,
        BrandGrpcClient brandClient)
    {
        _sectionClient = sectionClient;
        _productClient = productClient;
        _brandClient = brandClient;
    }

    public async Task<IViewComponentResult> InvokeAsync(string store)
    {
        int take = 9;
        int discountTake = 20;
        var item = await _sectionClient.GetSectionAndDetailsByStoreAsync(store);

        switch (item.SectionTypeId)
        {
            case 1:
                var slider = new SliderShowResponse();
                var desktop = new List<SectionDetailResponse>();
                var mobile = new List<SectionDetailResponse>();

                foreach (var slide in item.SectionDetails)
                {
                    if (slide.ResponsiveSize == "Mobile")
                    {
                        mobile.Add(slide);
                    }
                    else
                    {
                        desktop.Add(slide);
                    }
                }

                slider.Desktop.AddRange(desktop);
                slider.Mobile.AddRange(mobile);
                return View("SlideShow", slider);

            case 2:
                if (item.CateguryId != 0)
                {
                    var parameter = new ProductSearchParameterRequest { CateguryId = item.CateguryId, Take = 10 };

                    #region Get User If Authenticated

                    string userName = null;
                    if (User.Identity.IsAuthenticated)
                    {
                        userName = User.FindFirstValue(ClaimTypes.Name);
                    }

                    #endregion

                    var pagingItem = await _productClient.GetIntegratedProductsPagingAsync(parameter, userName);
                    item.ProductCateguries.AddRange(pagingItem.ProductList);

                    if (item.ProductCateguries.Count > 0)
                    {
                        item.LatinCateguryName = item.ProductCateguries[0].CateguryLatinName;
                    }
                }

                return View("ListProducts", item);

            case 3:
                var discountProducts = await _productClient.GetProductsWithDiscountAsync(discountTake, item.Id);
                item.ProductCateguries.AddRange(discountProducts);

                if (item.ProductId != 0)
                {
                    var product = await _productClient.GetProductByIdAsync(item.ProductId);
                    item.Product = product;
                }

                return View("Discount", item);

            case 4: return View("TwoBanner", item);
            case 5: return View("SixBanner", item);
            case 6: return View("LargeBanner", item);
            case 7: return View("Icons", item);
            case 8:
                if (item.CateguryId != 0 && item.ProductCateguries.Count > 0)
                {
                    item.LatinCateguryName = item.ProductCateguries[0].CateguryLatinName;
                }

                return View("CateguryIcons", item);
            case 9:
                var brands = await _brandClient.GetAllBrandsAsync();
                item.Brands.AddRange(brands.Brands);
                return View("BrandSlider", item);
        }

        return View("Default");
    }
}