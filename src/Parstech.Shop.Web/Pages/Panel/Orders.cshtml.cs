using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Context.Application.DTOs.Order;
using Parstech.Shop.Context.Application.DTOs.Paging;
using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.Features.Order.Requests.Queries;
using Parstech.Shop.Context.Application.Features.OrderDetail.Requests.Queries;
using Parstech.Shop.Context.Application.Features.User.Requests.Queries;

namespace Parstech.Shop.Web.Pages.Panel;

[Authorize]
public class OrdersModel : PageModel
{
    #region Constractor

    private readonly IMediator _mediator;
    public OrdersModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion
    #region Properties


    [BindProperty]
    public ParameterDto Parameter { get; set; } = new ParameterDto();

    [BindProperty]
    public PagingDto List { get; set; }

    [BindProperty]
    public OrderDto OrderDto { get; set; }

    [BindProperty]
    public List<OrderDto> Orders { get; set; }

    [BindProperty]
    public int OrderId { get; set; }

    [BindProperty]
    public ResponseDto Response { get; set; } = new();

    [BindProperty]
    public OrderDetailShowDto OrderDetailShowDto { get; set; }





    #endregion
    #region Get
    public async Task<IActionResult> OnGet()
    {
        return Page();
    }
    public async Task<IActionResult> OnPostData()
    {
        Parameter.CurrentPage = 1;
        Parameter.TakePage = 30;
        var CurrentUser = await _mediator.Send(new UserReadByUserNameQueryReq(User.Identity.Name));
        List = await _mediator.Send(new FinallyOrdersOfUserByPagingQueryReq(CurrentUser.Id, Parameter));
        Response.Object = List;
        Response.IsSuccessed = true;

        return new JsonResult(Response);
    }
    #endregion
    #region Search Paging

    public async Task<IActionResult> OnPostSearch()
    {
        Parameter.CurrentPage = 1;
        Parameter.TakePage = 30;
        var CurrentUser = await _mediator.Send(new UserReadByUserNameQueryReq(User.Identity.Name));
        List = await _mediator.Send(new FinallyOrdersOfUserByPagingQueryReq(CurrentUser.Id, Parameter));
        Response.Object = List;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostPaging()
    {
        Parameter.TakePage = 30;
        var CurrentUser = await _mediator.Send(new UserReadByUserNameQueryReq(User.Identity.Name));
        List = await _mediator.Send(new FinallyOrdersOfUserByPagingQueryReq(CurrentUser.Id, Parameter));
        Response.Object = List;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }


    #endregion
    #region Show Order Detail

    public async Task<IActionResult> OnPostShowOrderDetail()
    {
        OrderDetailShowDto = await _mediator.Send(new OrderDetailShowQueryReq(OrderId));

        Response.Object = OrderDetailShowDto;
        return new JsonResult(Response);
    }

    #endregion
}