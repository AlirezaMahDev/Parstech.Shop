using Azure;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Context.Application.DTOs.Order;
using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.Features.Order.Requests.Queries;

namespace Parstech.Shop.Web.Pages.Panel;

[Authorize]
public class ShopingCartModel : PageModel
{
    #region Constractor

    private readonly IMediator _mediator;

    public ShopingCartModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    #region Properties

    [BindProperty]
    public OrderDetailShowDto ShoppingCart { get; set; }

    [BindProperty]
    public ResponseDto Response { get; set; } = new ResponseDto();

    [BindProperty]
    public int UserId { get; set; }

    #endregion

    #region Get

    public async Task<IActionResult> OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostData()
    {
        ShoppingCart = await _mediator.Send(new NotFinallyOrderOfUserQueryReq(UserId));
        Response.Object = ShoppingCart;
        Response.IsSuccessed = true;

        return new JsonResult(Response);
    }

    #endregion
}