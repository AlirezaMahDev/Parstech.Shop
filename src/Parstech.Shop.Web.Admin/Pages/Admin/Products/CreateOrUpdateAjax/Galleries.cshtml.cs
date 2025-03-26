using AutoMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.ProductGallery;
using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.Features.Product.Requests.Commands;
using Parstech.Shop.Context.Application.Features.ProductGallery.Requests.Commands;
using Parstech.Shop.Context.Application.Features.ProductGallery.Requests.Queries;
using Parstech.Shop.Context.Application.Validators.ProductGallery;

namespace Parstech.Shop.Web.Admin.Pages.Admin.Products.CreateOrUpdateAjax;

public class EditContentModel : PageModel
{

    #region Constractor

    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IProductStockPriceRepository _productStockRep;


    public EditContentModel(IMediator mediator,
        IMapper mapper,
        IProductStockPriceRepository productStockRep)
    {
        _mediator = mediator;
        _mapper = mapper;
        _productStockRep = productStockRep;
    }

    #endregion
    //id
    [BindProperty] public int? productId { get; set; }


    //product
    [BindProperty] public List<ProductGalleryDto> GalleryDtos { get; set; }


    //gallery
    [BindProperty] public ProductGalleryDto Gallery { get; set; }


    [BindProperty]
    public UploadViewModel UploadData { get; set; }

    [BindProperty] public int GalleryId { get; set; }

    //result
    [BindProperty] public ResponseDto Response { get; set; } = new();


    public async Task OnGet(int? id)
    {
        productId = id;
        if (productId != null)
        {
            GalleryDtos = await _mediator.Send(new GalleriesOfProductQueryReq(productId.Value));
        }
    }

    public async Task<IActionResult> OnPostAddGallery()
    {

        #region Validator

        var validator = new ProductGalleryDtoValidator();
        var valid = validator.Validate(Gallery);
        if (!valid.IsValid)
        {
            Response.IsSuccessed = false;
            Response.Errors = valid.Errors;
            Response.Object = Gallery;
            return new JsonResult(Response);
        }

        #endregion


        var res=await _mediator.Send(new ProductGalleryCreateCommandReq(Gallery));

        #region NotActive Product When Role Is Store
        if (User.IsInRole("Store"))
        {
            var currtentProduct = await _mediator.Send(new ProductReadCommandReq(Gallery.ProductId));
            currtentProduct.IsActive = false;
            var p = await _mediator.Send(new ProductUpdateCommandReq(currtentProduct));

        }
        #endregion

        //Response.IsSuccessed = true;
        //Response.Message = "تصویر محصول با موفقیت بارگذاری گردید.";
        //Response.Object = Gallery;

        return new JsonResult(res);
    }

    //public async Task<IActionResult> OnPostAddMultipleGallery()
    //{
    //    if (UploadData?.Files == null || UploadData.Files.Count == 0)
    //    {
    //        ModelState.AddModelError("", "لطفاً حداقل یک فایل انتخاب کنید.");
    //        return Page();
    //    }

    //    await _mediator.Send(new ProductGalleryMultipleCreateCommandReq(UploadData, Gallery.ProductId));

    //    #region NotActive Product When Role Is Store
    //    if (User.IsInRole("Store"))
    //    {
    //        var currtentProduct = await _mediator.Send(new ProductReadCommandReq(Gallery.ProductId));
    //        currtentProduct.IsActive = false;
    //        var p = await _mediator.Send(new ProductUpdateCommandReq(currtentProduct));

    //    }
    //    #endregion

    //    Response.IsSuccessed = true;
    //    Response.Message = "تصویر محصول با موفقیت بارگذاری گردید.";
    //    Response.Object = Gallery;

    //    return new JsonResult(Response);
    //}

    public async Task<IActionResult> OnPostDeleteGallery()
    {
        await _mediator.Send(new ProductGalleryDeleteCommandReq(GalleryId));
        #region NotActive Product When Role Is Store
        if (User.IsInRole("Store"))
        {
            var currtentProduct = await _mediator.Send(new ProductReadCommandReq(Gallery.ProductId));
            currtentProduct.IsActive = false;
            var p = await _mediator.Send(new ProductUpdateCommandReq(currtentProduct));

        }
        #endregion
        Response.IsSuccessed = true;
        Response.Message = "تصویر محصول با موفقیت حذف گردید.";
        Response.Object = Gallery;

        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostChangeMainGallery()
    {
        var Response=await _mediator.Send(new ChangeMainGalleryCommandReq(GalleryId, productId.Value));

        return new JsonResult(Response);
    }
}