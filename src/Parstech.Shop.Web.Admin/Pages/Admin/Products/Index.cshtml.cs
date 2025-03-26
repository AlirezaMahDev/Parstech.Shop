using AutoMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Context.Application.DTOs.Brand;
using Parstech.Shop.Context.Application.DTOs.Product;
using Parstech.Shop.Context.Application.DTOs.ProductCategury;
using Parstech.Shop.Context.Application.DTOs.ProductGallery;
using Parstech.Shop.Context.Application.DTOs.ProductProperty;
using Parstech.Shop.Context.Application.DTOs.ProductRepresentation;
using Parstech.Shop.Context.Application.DTOs.ProductStockPrice;
using Parstech.Shop.Context.Application.DTOs.ProductType;
using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.DTOs.Tax;
using Parstech.Shop.Context.Application.DTOs.UserStore;
using Parstech.Shop.Context.Application.Features.Brand.Requests.Commands;
using Parstech.Shop.Context.Application.Features.Categury.Requests.Commands;
using Parstech.Shop.Context.Application.Features.Product.Requests.Commands;
using Parstech.Shop.Context.Application.Features.Product.Requests.Queries;
using Parstech.Shop.Context.Application.Features.ProductCategury.Requests.Commands;
using Parstech.Shop.Context.Application.Features.ProductCategury.Requests.Queries;
using Parstech.Shop.Context.Application.Features.ProductGallery.Requests.Commands;
using Parstech.Shop.Context.Application.Features.ProductGallery.Requests.Queries;
using Parstech.Shop.Context.Application.Features.ProductProperty.Requests.Commands;
using Parstech.Shop.Context.Application.Features.ProductProperty.Requests.Queries;
using Parstech.Shop.Context.Application.Features.ProductRepresentation.Requests.Queries;
using Parstech.Shop.Context.Application.Features.ProductStockPrice.Requests.Queries;
using Parstech.Shop.Context.Application.Features.ProductType.Requests.Commands;
using Parstech.Shop.Context.Application.Features.Tax.Requests.Commands;
using Parstech.Shop.Context.Application.Features.UserStore.Requests.Commands;

namespace Parstech.Shop.Web.Admin.Pages.Admin.Products;

[Authorize(Roles = "SupperUser,Sale,Store")]
public class IndexModel : PageModel
{
    #region Constractor

    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public IndexModel(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    #endregion

    #region Properties

    //paging parameter
    [BindProperty]
    public ProductParameterDto Parameter { get; set; } = new();


    //id
    [BindProperty]
    public int productId { get; set; }

    //products
    [BindProperty]
    public ProductPageingDto List { get; set; }

    //product
    [BindProperty]
    public ProductQuickEditDto ProductQuickEditDto { get; set; }

    [BindProperty]
    public ProductDto ProductDto { get; set; }

    //result
    [BindProperty]
    public ResponseDto Response { get; set; } = new();


    //gallery
    [BindProperty]
    public ProductGalleryDto Gallery { get; set; }

    [BindProperty]
    public List<ProductGalleryDto> Galleries { get; set; }

    //property
    [BindProperty]
    public ProductPropertyDto property { get; set; }

    [BindProperty]
    public List<ProductPropertyDto> properties { get; set; }

    //categury
    [BindProperty]
    public ProductCateguryDto categury { get; set; }

    [BindProperty]
    public List<ProductCateguryDto> categuries { get; set; }

    [BindProperty]
    public string FilterCat { get; set; }

    //rep
    [BindProperty]
    public ProductRepresentationDto rep { get; set; }


    [BindProperty]
    public int Type { get; set; }

    [BindProperty]
    public string Filter { get; set; }


    public List<ProductTypeDto> ProducyTypes { get; set; }
    public List<TaxDto> TaxList { get; set; }
    public List<BrandDto> Brands { get; set; }
    public List<UserStoreDto> UserStores { get; set; }
    #endregion

    #region Get

    public async Task<IActionResult> OnGet()
    {
        ProducyTypes = await _mediator.Send(new ProductTypeReadsCommandReq());
        TaxList = await _mediator.Send(new TaxReadsCommandReq());
        Brands = await _mediator.Send(new BrandReadsCommandReq());
        UserStores = await _mediator.Send(new UserStoreReadsCommandReq());
        return Page();
    }

    public async Task<IActionResult> OnPostGetData()
    {
        if (Parameter.CurrentPage == 0)
        {
            Parameter.CurrentPage=1;
        }
        //Parameter.CurrentPage = 1;
        Parameter.TakePage = 30;
        List = await _mediator.Send(new ProductPagingForAdminQueryReq(Parameter));

        if (User.IsInRole("Store") || User.IsInRole("StoreBySend"))
        {
            Response.Object2 = "Store";
        }
        else
        {
            Response.Object2 = "All";
        }

        Response.Object = List;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    #endregion
    #region Search Paging

    public async Task<IActionResult> OnPostSearch()
    {
        //Parameter.CurrentPage = 1;
        Parameter.TakePage = 30;
        List = await _mediator.Send(new ProductPagingQueryReq(Parameter));
        Response.Object = List;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostPaging()
    {
        Parameter.TakePage = 30;
        List = await _mediator.Send(new ProductPagingQueryReq(Parameter));
        Response.Object = List;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    #endregion


    #region EditProduct

    public async Task<IActionResult> OnPostProduct()
    {
        ProductDto = await _mediator.Send(new ProductReadCommandReq(productId));
        Response.Object = ProductDto;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostEditOrCreateProduct()
    {
        if (ProductDto.Id != 0)
        {
            var pquickDto = _mapper.Map<ProductQuickEditDto>(ProductDto);
            await _mediator.Send(new ProductQuickEditQueryReq(pquickDto));
            Response.Object = ProductQuickEditDto;
            Response.IsSuccessed = true;
            Response.Message = "محصول با موفقیت ویرایش شد";
            return new JsonResult(Response);
        }
        else
        {
            await _mediator.Send(new ProductCreateCommandReq(ProductDto));
            Response.Object = ProductDto;
            Response.IsSuccessed = true;
            Response.Message = "محصول با موفقیت ثبت شد";
            return new JsonResult(Response);
        }

    }
    public async Task<IActionResult> OnPostDuplicateForStoreProduct(int productId, int storeId)
    {
        await _mediator.Send(new ProductDuplicateForStoreQueryReq(productId, storeId));
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }
    public async Task<IActionResult> OnPostDuplicateProduct(int productId)
    {
        await _mediator.Send(new ProductDuplicateQueryReq(productId));
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }
    #endregion


    #region Gallery

    public async Task<IActionResult> OnPostGalleries()
    {
        Galleries = await _mediator.Send(new GalleriesOfProductQueryReq(productId));
        Response.Object = Galleries;
        return new JsonResult(Response);
    }
    public async Task<IActionResult> OnPostGallery()
    {
        Gallery = await _mediator.Send(new GalleryOfProductQueryReq(productId));
        Response.Object = Gallery;
        return new JsonResult(Response);
    }
    public async Task<IActionResult> OnPostEditOrCreateGallery()
    {
        if (Gallery.Id != 0)
        {
            await _mediator.Send(new ProductGalleryUpdateCommandReq(Gallery));
            Response.Object = Gallery;
            Response.IsSuccessed = true;
            Response.Message = "تصویر محصول با موفقیت ویرایش شد";
            return new JsonResult(Response);
        }
        else
        {
            await _mediator.Send(new ProductGalleryCreateCommandReq(Gallery));
            Response.Object = Gallery;
            Response.IsSuccessed = true;
            Response.Message = "تصویر محصول با موفقیت ثبت شد";
            return new JsonResult(Response);
        }
    }

    #endregion

    #region Rep

    public async Task<IActionResult> OnPostProductRep()
    {
        rep = await _mediator.Send(new ProductRepresentationOfProductQueryReq(productId));
        Response.Object = rep;
        return new JsonResult(Response);
    }


    #endregion
    #region Categury

    public async Task<IActionResult> OnPostCateguries()
    {
        categuries = await _mediator.Send(new CateguriesOfProductQueryReq(productId));
        Response.Object = categuries;
        return new JsonResult(Response);
    }
    public async Task<IActionResult> OnPostCategury()
    {
        categury = await _mediator.Send(new CateguryOfProductQueryReq(productId));
        Response.Object = categury;
        return new JsonResult(Response);
    }
    public async Task<IActionResult> OnPostDeleteCategury()
    {
        var ProductId = await _mediator.Send(new ProductCateguryDeleteCommandReq(productId));
        Response.Object = ProductId;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }


    public async Task<IActionResult> OnPostGetAllCateguries()
    {
        var categuries = await _mediator.Send(new CateguryReadCommandReq(FilterCat));
        Response.Object = categuries;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }
    public async Task<IActionResult> OnPostEditOrCreateCategury()
    {
        if (categury.Id != 0)
        {
            await _mediator.Send(new ProductCateguryUpdateCommandReq(categury));
            Response.Object = categury;
            Response.IsSuccessed = true;
            Response.Message = "دسته بندی محصول با موفقیت ویرایش شد";
            return new JsonResult(Response);
        }
        else
        {
            await _mediator.Send(new ProductCateguryCreateCommandReq(categury));
            Response.Object = categury;
            Response.IsSuccessed = true;
            Response.Message = "دسته بندی محصول با موفقیت ثبت شد";
            return new JsonResult(Response);
        }

    }

    #endregion

    #region Property

    public async Task<IActionResult> OnPostProperties()
    {
        properties = await _mediator.Send(new PropertiesOfProductQueryReq(productId));
        Response.Object = properties;
        return new JsonResult(Response);
    }
    public async Task<IActionResult> OnPostProperty()
    {
        property = await _mediator.Send(new PropertyOfProductQueryReq(productId));
        Response.Object = property;
        return new JsonResult(Response);
    }
    public async Task<IActionResult> OnPostEditOrCreateProperty()
    {
        if (property.Id != 0)
        {
            await _mediator.Send(new ProductPropertyUpdateCommandReq(property));
            Response.Object = property;
            Response.IsSuccessed = true;
            Response.Message = "ویژگی محصول با موفقیت ویرایش شد";
            return new JsonResult(Response);
        }
        else
        {
            await _mediator.Send(new ProductPropertyCreateCommandReq(property));
            Response.Object = property;
            Response.IsSuccessed = true;
            Response.Message = "ویژگی محصول با موفقیت ثبت شد";
            return new JsonResult(Response);
        }

    }

    #endregion

    public async Task<IActionResult> OnPostProductParents()
    {
        if (Type == 1)
        {
            var result = await _mediator.Send(new GetAllParentVariableProductQueryReq(Filter));
            Response.Object = result;
        }
        else if (Type == 2)
        {
            var result = await _mediator.Send(new GetAllParentBundleProductQueryReq(Filter));
            Response.Object = result;

        }

        return new JsonResult(Response);
    }

    #region Delete
    public async Task<IActionResult> OnPostDelete()
    {
        var result = await _mediator.Send(new ProductDeleteQueryReq(productId));

        Response.IsSuccessed = result;
        return new JsonResult(Response);
    }
    #endregion

    #region Add product For Store

    [ValidateAntiForgeryToken]
    public async Task<JsonResult> OnPostSearchProduct(string Filter)
    {
        var list = await _mediator.Send(new SearchProductQueryReq(Filter, 30));
        Response.Object = list;
        Response.IsSuccessed = true;
        return new(Response);
    }

    #endregion
    #region Get Product Stores

    [ValidateAntiForgeryToken]
    public async Task<JsonResult> OnPostProductStores(int ProductId)
    {
        if (User.IsInRole("Store") || User.IsInRole("StoreBySend"))
        {
            var list = new List<ProductStockPriceStoreDto>();
            ProductStockPriceStoreDto item = new()
            {
                Id = 0,
                StoreName = "دسترسی ندارید"
            };
            list.Add(item);
            Response.Object = list;
            Response.IsSuccessed = true;
        }
        else
        {
            var list = await _mediator.Send(new GetProductStockPriceStoreOfProductQueryReq(ProductId));
            Response.Object = list;
            Response.IsSuccessed = true;
        }

        return new(Response);
    }

    #endregion

}