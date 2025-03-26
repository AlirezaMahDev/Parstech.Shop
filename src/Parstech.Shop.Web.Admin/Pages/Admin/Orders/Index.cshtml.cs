using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Context.Application.DTOs.Order;
using Parstech.Shop.Context.Application.DTOs.OrderPay;
using Parstech.Shop.Context.Application.DTOs.OrderShipping;
using Parstech.Shop.Context.Application.DTOs.OrderStatus;
using Parstech.Shop.Context.Application.DTOs.Paging;
using Parstech.Shop.Context.Application.DTOs.PayType;
using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.DTOs.Status;
using Parstech.Shop.Context.Application.Features.Order.Requests.Queries;
using Parstech.Shop.Context.Application.Features.OrderDetail.Requests.Queries;
using Parstech.Shop.Context.Application.Features.OrderPay.Request.Command;
using Parstech.Shop.Context.Application.Features.OrderPay.Request.Queries;
using Parstech.Shop.Context.Application.Features.OrderShipping.Request.Queries;
using Parstech.Shop.Context.Application.Features.OrderStatus.Requests.Commands;
using Parstech.Shop.Context.Application.Features.OrderStatus.Requests.Queries;

namespace Parstech.Shop.Web.Admin.Pages.Admin.Orders;

[Authorize(Roles = "SupperUser,Sale,Store,Finanicial")]
public class IndexModel : PageModel
{

    #region Constractor

    private readonly IMediator _mediator;

    public IndexModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    #region Properties

    [BindProperty]
    public ParameterDto Parameter { get; set; } = new();

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
    [BindProperty]
    public List<StatusDto> Statuses { get; set; }

    [BindProperty]
    public List<PayTypeDto> payTypes { get; set; }
    [BindProperty]
    public OrderStatusDto OrderStatusDto { get; set; }
    [BindProperty]
    public OrderShippingDto OrderShippingDto { get; set; }
    [BindProperty]
    public OrderShippingChangeDto orderShippingChangeDto { get; set; }
    [BindProperty]
    public int UserShippingId { get; set; }

    [BindProperty]
    public OrderPayDto orderPayDto { get; set; }


    [BindProperty]
    public OrderFilterDto orderFilterDto { get; set; }




    #endregion

    #region Get

    public async Task<IActionResult> OnGet()

    {
        if (User.IsInRole("Store"))
        {
            orderFilterDto = await _mediator.Send(new OrdersFilterDataQueryReq(User.Identity.Name));

        }
        else
        {
            orderFilterDto = await _mediator.Send(new OrdersFilterDataQueryReq(null));
        }


        return Page();
    }

    public async Task<IActionResult> OnPostData()
    {

        Parameter.TakePage = 30;
        if (User.IsInRole("Store"))
        {
            Parameter.store = User.Identity.Name;
        }
        else
        {
            Parameter.store = null;

        }

        List = await _mediator.Send(new OrderPagingQueryReq(Parameter));
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
        if (User.IsInRole("Store"))
        {
            Parameter.store = User.Identity.Name;
        }
        else
        {
            Parameter.store = null;

        }
        List = await _mediator.Send(new OrderPagingQueryReq(Parameter));
        Response.Object = List;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostPaging()
    {
        Parameter.TakePage = 30;
        if (User.IsInRole("Store"))
        {
            Parameter.store = User.Identity.Name;
        }
        else
        {
            Parameter.store = null;

        }
        List = await _mediator.Send(new OrderPagingQueryReq(Parameter));
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
    #region Status Change

    public async Task<IActionResult> OnPostStatusChange(IFormFile file)
    {
        OrderStatusDto.CreateDate = DateTime.Now;
        OrderStatusDto.CreateBy = User.Identity.Name;
        OrderStatusDto.File = file;
        Response = await _mediator.Send(new OrderStatusCreatCommandReq(OrderStatusDto));

        return new JsonResult(Response);
    }

    #endregion

    #region Order Edit

    public async Task<IActionResult> OnPostOrderShippingChange()
    {

        var orderId = await _mediator.Send(new OrderShippingChangeQueryReq("Change", UserShippingId, OrderId, 0));
        Response.Object = orderId;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    #endregion
    #region Word File
    public async Task<IActionResult> OnPostOrderWord()
    {
        OrderDetailShowDto = await _mediator.Send(new OrderDetailShowQueryReq(OrderId));
        var word = await _mediator.Send(new OrderWordFileQueryReq(OrderDetailShowDto));


        //Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();


        //object oMissing = System.Reflection.Missing.Value;


        //DirectoryInfo dirInfo = new DirectoryInfo("/wwwroot/Shared/Factors");
        //FileInfo[] wordFiles = dirInfo.GetFiles("*.docx");

        //word.Visible = false;
        //word.ScreenUpdating = false;

        //foreach (FileInfo wordFile in wordFiles)
        //{

        //    Object filename2 = (Object)wordFile.FullName;


        //    Microsoft.Office.Interop.Word.Document doc2 = word.Documents.Open(ref filename2, ref oMissing,
        //        ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
        //        ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
        //        ref oMissing, ref oMissing, ref oMissing, ref oMissing);
        //    doc2.Activate();

        //    object outputFileName = wordFile.FullName.Replace(".doc", ".pdf");
        //    object fileFormat = WdSaveFormat.wdFormatPDF;


        //    doc2.SaveAs(ref outputFileName,
        //        ref fileFormat, ref oMissing, ref oMissing,
        //        ref oMissing, ref oMissing, ref oMissing, ref oMissing,
        //        ref oMissing, ref oMissing, ref oMissing, ref oMissing,
        //        ref oMissing, ref oMissing, ref oMissing, ref oMissing);


        //    object saveChanges = WdSaveOptions.wdDoNotSaveChanges;
        //    ((_Document)doc2).Close(ref saveChanges, ref oMissing, ref oMissing);
        //    doc2 = null;
        //}
        //((_Application)word).Quit(ref oMissing, ref oMissing, ref oMissing);
        //word = null;

        return Redirect("/" + word);
    }
    #endregion

    #region Get Statuses

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostGetStatuses(int OrderId)
    {

        var list = await _mediator.Send(new GetOrderStatusByOrderIdQueryReq(OrderId));
        Response.Object = list;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    #endregion
    #region CompleteOrder
    public async Task<IActionResult> OnPostComplete(int orderId, string typeName, int? month)
    {
        var res = await _mediator.Send(new CompleteOrderByAdminQueryReq(orderId, typeName, month));
        return new JsonResult(res);


    }

    public async Task<IActionResult> OnPostOrderPays(int orderId)
    {
        var res = await _mediator.Send(new OrderPaysOfOrderQueryReq(orderId));
        return new JsonResult(res);
    }
    public async Task<IActionResult> OnPostAddOrderPay()
    {
        var res = await _mediator.Send(new OrderPayCreateCommandReq(orderPayDto));
        return new JsonResult(res);
    }

    public async Task<IActionResult> OnPostDeleteOrderPay(int id)
    {
        var res = await _mediator.Send(new OrderPayDeleteCommandReq(id));
        return new JsonResult(res);
    }
    #endregion
}