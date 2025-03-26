using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Context.Application.DTOs.Rahkaran;
using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.Features.Rahkaran.Requests.Commands;
using Parstech.Shop.Context.Application.Features.Rahkaran.Requests.Queries;

namespace Parstech.Shop.Web.Admin.Pages.Admin.Orders;

[Authorize(Roles = "SupperUser,Sale,Store,Finanicial")]
public class ApiHamkaranModel : PageModel
{

    #region Constractor

    private readonly IMediator _mediator;

    public ApiHamkaranModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    #region Properties

    [BindProperty]
    public int OrderId { get; set; }

    [BindProperty]
    public RahkaranAllDto Response { get; set; } = new();



    [BindProperty]
    public RahkaranOrderDto orderDto { get; set; } = new();

    [BindProperty]
    public RahkaranUserDto userDto { get; set; } = new();

    [BindProperty]
    public RahkaranProductDto productDto { get; set; } = new();


    [BindProperty]
    public ResponseDto result { get; set; } = new();

    #endregion

    #region Get

    public async Task<IActionResult> OnGet(int id)

    {
        OrderId = id;
        return Page();
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostData(int orderId)
    {
        Response = await _mediator.Send(new RahakaranAllQueryReq(orderId));

        return new JsonResult(Response);
    }



    #endregion

    #region order
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostOrder(int id)

    {
        var res = await _mediator.Send(new RahkaranOrderReadCommandReq(id));

        return new JsonResult(res);
    }


    public async Task<IActionResult> OnPostAEOrder(int type)
    {
        if (type == 1)
        {
            var res = await _mediator.Send(new RahkaranOrderCreateCommandReq(orderDto));

            return new JsonResult(res);
        }
        else
        {
            var res = await _mediator.Send(new RahkaranOrderUpdateCommandReq(orderDto));

            return new JsonResult(res);
        }

    }
    #endregion

    #region user
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostUser(int id)

    {
        var res = await _mediator.Send(new RahkaranUserReadCommandReq(id));

        return new JsonResult(res);
    }


    public async Task<IActionResult> OnPostAEUser(int type)
    {
        if (type == 1)
        {
            var res = await _mediator.Send(new RahkaranUserCreateCommandReq(userDto));

            return new JsonResult(res);
        }
        else
        {
            var res = await _mediator.Send(new RahkaranUserUpdateCommandReq(userDto));

            return new JsonResult(res);
        }

    }
    #endregion

    #region product
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostProduct(int id)

    {
        var res = await _mediator.Send(new RahkaranProductReadCommandReq(id));

        return new JsonResult(res);
    }


    public async Task<IActionResult> OnPostAEProduct(int type)
    {
        if (type == 1)
        {
            var res = await _mediator.Send(new RahkaranProductCreateCommandReq(productDto));

            return new JsonResult(res);
        }
        else
        {
            var res = await _mediator.Send(new RahkaranProductUpdateCommandReq(productDto));

            return new JsonResult(res);
        }

    }
    #endregion

    #region Send Api
    public async Task<IActionResult> OnPostSendOrder(int orderId)
    {
        result = await _mediator.Send(new RahkaranSendOrderToApiQueryReq(orderId));
        return new JsonResult(result);
    }

    public async Task<IActionResult> OnPostFollowOrder(int orderId)
    {
        result = await _mediator.Send(new RahkaranFollowOrderFromApiQueryReq(orderId));
        return new JsonResult(result);

    }
    #endregion

}