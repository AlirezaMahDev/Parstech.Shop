using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Context.Application.DTOs.Api;
using Parstech.Shop.Context.Application.DTOs.Product;
using Parstech.Shop.Context.Application.DTOs.ProductGallery;
using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.Features.Api.Requests.Queries;
using Parstech.Shop.Context.Application.Features.Product.Requests.Queries;
using Parstech.Shop.Context.Application.Features.ProductGallery.Requests.Queries;

namespace Parstech.Shop.Web.Pages.Products;

public class DetailModel : PageModel
{
    #region Constractor

    private readonly IMediator _mediator;

    public DetailModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    #region Properties

    //result
    [BindProperty]
    public ResponseDto Response { get; set; } = new();


    [BindProperty]
    public ProductDetailShowDto Item { get; set; } = new();


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
        ShortLink = shortLink;
        StoreId = storeId;
        var product = await _mediator.Send(new GetProductByShortLinkQueryReq(shortLink));
        Galleries = await _mediator.Send(new GalleriesOfProductQueryReq(product.Id));

        #region Torob
        string baseUrl = $"{Request.Scheme}://{Request.Host.ToUriComponent()}";
        Torob = await _mediator.Send(new TorobGetProductQueryReq(storeId, baseUrl));
        #endregion

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
        #endregion
        Item = await _mediator.Send(new ProductDetailShowQueryReq(ShortLink, StoreId, userName));
        //Response.Object = Item;
        //Response.IsSuccessed = true;
        //return new JsonResult(Response);
        return Page();
    }

    public async Task<IActionResult> OnPostData()
    {
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
        #endregion
        var Item = await _mediator.Send(new ProductDetailShowQueryReq(ShortLink, StoreId,userName));
        Response.Object = Item;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    #endregion
}