using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.Section;
using Shop.Application.Features.Brand.Requests.Commands;
using Shop.Application.Features.Product.Requests.Commands;
using Shop.Application.Features.Product.Requests.Queries;
using Shop.Application.Features.Section.Requests.Queries;

namespace Shop.Web.ViewComponents
{
    [ViewComponent(Name = "StorePageSection")]
    public class StorePageSectionViewComponent : ViewComponent
    {

        private readonly IMediator _mediator;
        public StorePageSectionViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IViewComponentResult> InvokeAsync(string store)
        {
            int Take = 9;
            int DiscountTake = 20;
            var Item = await _mediator.Send(new SectionAndDetailsReadStoreQueryReq(store));

            switch (Item.SectionTypeId)
            {
                case 1:
                    SliderShowDto slider = new SliderShowDto();
                    List<SectionDetailShowDto> desktop = new List<SectionDetailShowDto>();
                    List<SectionDetailShowDto> mobile = new List<SectionDetailShowDto>();
                    foreach (var slide in Item.SectionDetails)
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
                    slider.Desktop = desktop;
                    slider.Mobile = mobile;
                    return View("SlideShow", slider);

                case 2:
                    if (Item.CateguryId != 0)
                    {
                        ProductSearchParameterDto parameter = new ProductSearchParameterDto();
                        parameter.CateguryId = Item.CateguryId;
                        parameter.Take = 10;
                        #region Get User If Authenticated
                        var userName = "";
                        if (User.Identity.IsAuthenticated)
                        {
                            userName = User.Identity.Name;
                        }
                        else
                        {
                            userName = null;
                        }
                        #endregion'
                        var pagingItem = await _mediator.Send(new IntegratedProductsPagingQueryReq(parameter,userName));
                        Item.ProductCateguries = pagingItem.ProductList;
                        //Item.ProductCateguries = await _mediator.Send(new GetSomeOfLastProductsByCateguryIdQueryReq(Take, Item.CateguryId));
                        Item.latinCateguryName = Item.ProductCateguries.First().CateguryLatinName;
                    }
                    return View("ListProducts", Item);

                case 3:

                    Item.ProductCateguries = await _mediator.Send(new GetProductsWithDiscountQueryReq(DiscountTake,Item.Id));

                    if (Item.ProductId != 0)
                    {
                        Item.Product = await _mediator.Send(new ProductReadCommandReq(Item.ProductId));
                    }
                    return View("Discount", Item);
                case 4: return View("TwoBanner", Item);
                case 5: return View("SixBanner", Item);
                case 6: return View("LargeBanner", Item);
                case 7: return View("Icons", Item);
                case 8:
                    if (Item.CateguryId != 0)
                    {
                        Item.latinCateguryName = Item.ProductCateguries.First().CateguryLatinName;
                    }
                    return View("CateguryIcons", Item);
                case 9:
                    Item.Brands = await _mediator.Send(new BrandReadsCommandReq());
                    return View("BrandSlider", Item);
            }
            return View("Default");
        }
    }
}
