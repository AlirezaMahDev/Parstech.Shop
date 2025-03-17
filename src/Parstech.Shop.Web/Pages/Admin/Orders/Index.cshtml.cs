using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.Web.Services.GrpcClients;

namespace Parstech.Shop.Web.Pages.Admin.Orders;

[Authorize(Roles = "SupperUser,Sale,Store,Finanicial")]
public class IndexModel : PageModel
{
    #region Constructor

    private readonly IOrderAdminGrpcClient _orderAdminGrpcClient;

    public IndexModel(IOrderAdminGrpcClient orderAdminGrpcClient)
    {
        _orderAdminGrpcClient = orderAdminGrpcClient;
    }

    #endregion

    #region Properties

    [BindProperty]
    public OrderParameterDto Parameter { get; set; } = new OrderParameterDto();

    [BindProperty]
    public ResponsePagingDto<OrderDto, OrderParameterDto> List { get; set; }

    [BindProperty]
    public OrderDto OrderDto { get; set; }

    [BindProperty]
    public List<OrderDto> Orders { get; set; }

    [BindProperty]
    public int OrderId { get; set; }

    [BindProperty]
    public ResponseDto Response { get; set; } = new ResponseDto();

    [BindProperty]
    public OrderDetailDto OrderDetailShowDto { get; set; }

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
    public List<OrderFilterDataDto> OrderFilters { get; set; }

    #endregion

    #region Get

    public async Task<IActionResult> OnGet()
    {
        string filter = null;
        if (User.IsInRole("Store"))
        {
            filter = User.Identity.Name;
        }

        // Use OrderAdminGrpcClient
        OrderFilters = await _orderAdminGrpcClient.GetOrderFiltersAsync(filter);

        return Page();
    }

    public async Task<IActionResult> OnPostData()
    {
        Parameter.Take = 30;
        if (User.IsInRole("Store"))
        {
            Parameter.StoreName = User.Identity.Name;
        }

        // Use OrderAdminGrpcClient
        List = await _orderAdminGrpcClient.GetOrdersPagingAsync(Parameter);

        Response.Object = List;
        Response.IsSuccessed = true;

        return new JsonResult(Response);
    }

    #endregion

    #region Search Paging

    public async Task<IActionResult> OnPostSearch()
    {
        Parameter.PageId = 1;
        Parameter.Take = 30;
        if (User.IsInRole("Store"))
        {
            Parameter.StoreName = User.Identity.Name;
        }

        // Use OrderAdminGrpcClient
        List = await _orderAdminGrpcClient.GetOrdersPagingAsync(Parameter);

        Response.Object = List;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostPaging()
    {
        Parameter.Take = 30;
        if (User.IsInRole("Store"))
        {
            Parameter.StoreName = User.Identity.Name;
        }

        // Use OrderAdminGrpcClient
        List = await _orderAdminGrpcClient.GetOrdersPagingAsync(Parameter);

        Response.Object = List;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    #endregion

    #region Show Order Detail

    public async Task<IActionResult> OnPostShowOrderDetail()
    {
        // Use OrderAdminGrpcClient
        OrderDetailShowDto = await _orderAdminGrpcClient.GetOrderDetailsAsync(OrderId);

        Response.Object = OrderDetailShowDto;
        return new JsonResult(Response);
    }

    #endregion

    #region Status Change

    public async Task<IActionResult> OnPostStatusChange(IFormFile file)
    {
        if (OrderStatusDto == null)
        {
            OrderStatusDto = new OrderStatusDto();
        }

        OrderStatusDto.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        OrderStatusDto.IsCurrent = true;

        // Use OrderAdminGrpcClient
        var response = await _orderAdminGrpcClient.CreateOrderStatusAsync(OrderStatusDto, file);

        Response.IsSuccessed = response.IsSuccessed;
        Response.Message = response.Message;
        Response.Code = response.Code;
        Response.Object = response.Object;

        return new JsonResult(Response);
    }

    #endregion

    #region Order Shipping

    public async Task<IActionResult> OnPostShippingChange()
    {
        if (OrderShippingDto == null)
        {
            OrderShippingDto = new OrderShippingDto();
        }

        OrderShippingDto.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        OrderShippingDto.IsCurrent = true;

        // Use OrderAdminGrpcClient
        var response = await _orderAdminGrpcClient.ChangeOrderShippingAsync(OrderShippingDto);

        Response.IsSuccessed = response.IsSuccessed;
        Response.Message = response.Message;
        Response.Code = response.Code;
        Response.Object = response.Object;

        return new JsonResult(Response);
    }

    #endregion

    #region Document Generation

    public async Task<IActionResult> OnGetGenerateOrderWord(int id)
    {
        try
        {
            string fileName;
            byte[]? fileBytes = await _orderAdminGrpcClient.GenerateOrderWordFileAsync(id, out fileName);

            return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
        }
        catch (Exception ex)
        {
            Response.IsSuccessed = false;
            Response.Message = $"Error generating document: {ex.Message}";
            return new JsonResult(Response);
        }
    }

    #endregion

    #region Payment Management

    public async Task<IActionResult> OnPostGetOrderPays()
    {
        var payments = await _orderAdminGrpcClient.GetOrderPaysAsync(OrderId);

        Response.IsSuccessed = true;
        Response.Object = payments;

        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostAddOrderPay()
    {
        if (orderPayDto == null)
        {
            orderPayDto = new OrderPayDto();
        }

        orderPayDto.PaymentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        var response = await _orderAdminGrpcClient.AddOrderPayAsync(orderPayDto);

        Response.IsSuccessed = response.IsSuccessed;
        Response.Message = response.Message;
        Response.Code = response.Code;
        Response.Object = response.Object;

        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostDeleteOrderPay(int payId)
    {
        var response = await _orderAdminGrpcClient.DeleteOrderPayAsync(payId);

        Response.IsSuccessed = response.IsSuccessed;
        Response.Message = response.Message;
        Response.Code = response.Code;
        Response.Object = response.Object;

        return new JsonResult(Response);
    }

    #endregion

    #region Rahkaran Integration

    public async Task<IActionResult> OnPostSendOrderToRahkaran()
    {
        var response = await _orderAdminGrpcClient.SendOrderToRahkaranAsync(OrderId);

        Response.IsSuccessed = response.IsSuccessed;
        Response.Message = response.Message;
        Response.Code = response.Code;
        Response.Object = response.Object;

        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostFollowOrderFromRahkaran()
    {
        var response = await _orderAdminGrpcClient.FollowOrderFromRahkaranAsync(OrderId);

        Response.IsSuccessed = response.IsSuccessed;
        Response.Message = response.Message;
        Response.Code = response.Code;
        Response.Object = response.Object;

        return new JsonResult(Response);
    }

    #endregion

    #region Complete Order

    public async Task<IActionResult> OnPostCompleteOrder(string typeName, int month)
    {
        var response = await _orderAdminGrpcClient.CompleteOrderByAdminAsync(OrderId, typeName, month);

        Response.IsSuccessed = response.IsSuccessed;
        Response.Message = response.Message;
        Response.Code = response.Code;
        Response.Object = response.Object;

        return new JsonResult(Response);
    }

    #endregion
}