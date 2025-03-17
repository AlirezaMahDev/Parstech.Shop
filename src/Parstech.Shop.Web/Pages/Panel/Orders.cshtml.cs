using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.Web.Services.GrpcClients;

namespace Parstech.Shop.Web.Pages.Panel;

[Authorize]
public class OrdersModel : PageModel
{
    #region Constructor

    private readonly UserGrpcClient _userClient;
    private readonly UserProfileGrpcClient _userProfileClient;

    public OrdersModel(
        UserGrpcClient userClient,
        UserProfileGrpcClient userProfileClient)
    {
        _userClient = userClient;
        _userProfileClient = userProfileClient;
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
    public ResponseDto Response { get; set; } = new ResponseDto();

    [BindProperty]
    public Shop.Application.DTOs.OrderDetail.OrderDetailShowDto OrderDetailShowDto { get; set; }

    #endregion

    #region Get

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostData()
    {
        Parameter.CurrentPage = 1;
        Parameter.TakePage = 30;

        var currentUser = await _userClient.GetUserByUsernameAsync(User.Identity.Name);
        var ordersResponse = await _userProfileClient.GetUserOrdersHistoryAsync(currentUser.Id,
            Parameter.CurrentPage,
            Parameter.TakePage,
            Parameter.SearchKey);

        List = new PagingDto
        {
            CurrentPage = ordersResponse.CurrentPage,
            PageCount = ordersResponse.PageCount,
            RowCount = ordersResponse.TotalCount,
            List = ordersResponse.Orders.Select(o => new OrderDto
                {
                    OrderId = o.OrderId,
                    TrackingCode = o.TrackingCode,
                    CreateDate = DateTime.TryParse(o.CreateDate, out DateTime date) ? date : null,
                    IsPaid = o.IsPaid,
                    Total = o.Total,
                    Discount = o.Discount,
                    FinalPrice = o.FinalPrice,
                    Status = o.Status
                })
                .ToList()
        };

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

        var currentUser = await _userClient.GetUserByUsernameAsync(User.Identity.Name);
        var ordersResponse = await _userProfileClient.GetUserOrdersHistoryAsync(currentUser.Id,
            Parameter.CurrentPage,
            Parameter.TakePage,
            Parameter.SearchKey);

        List = new PagingDto
        {
            CurrentPage = ordersResponse.CurrentPage,
            PageCount = ordersResponse.PageCount,
            RowCount = ordersResponse.TotalCount,
            List = ordersResponse.Orders.Select(o => new OrderDto
                {
                    OrderId = o.OrderId,
                    TrackingCode = o.TrackingCode,
                    CreateDate = DateTime.TryParse(o.CreateDate, out DateTime date) ? date : null,
                    IsPaid = o.IsPaid,
                    Total = o.Total,
                    Discount = o.Discount,
                    FinalPrice = o.FinalPrice,
                    Status = o.Status
                })
                .ToList()
        };

        Response.Object = List;
        Response.IsSuccessed = true;

        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostPaging()
    {
        Parameter.TakePage = 30;

        var currentUser = await _userClient.GetUserByUsernameAsync(User.Identity.Name);
        var ordersResponse = await _userProfileClient.GetUserOrdersHistoryAsync(currentUser.Id,
            Parameter.CurrentPage,
            Parameter.TakePage,
            Parameter.SearchKey);

        List = new PagingDto
        {
            CurrentPage = ordersResponse.CurrentPage,
            PageCount = ordersResponse.PageCount,
            RowCount = ordersResponse.TotalCount,
            List = ordersResponse.Orders.Select(o => new OrderDto
                {
                    OrderId = o.OrderId,
                    TrackingCode = o.TrackingCode,
                    CreateDate = DateTime.TryParse(o.CreateDate, out DateTime date) ? date : null,
                    IsPaid = o.IsPaid,
                    Total = o.Total,
                    Discount = o.Discount,
                    FinalPrice = o.FinalPrice,
                    Status = o.Status
                })
                .ToList()
        };

        Response.Object = List;
        Response.IsSuccessed = true;

        return new JsonResult(Response);
    }

    #endregion

    #region Show Order Detail

    public async Task<IActionResult> OnPostShowOrderDetail()
    {
        var orderDetails = await _userProfileClient.GetOrderDetailsAsync(OrderId);

        OrderDetailShowDto = new Shop.Application.DTOs.OrderDetail.OrderDetailShowDto
        {
            OrderId = orderDetails.OrderId,
            UserName = orderDetails.UserName,
            Total = orderDetails.Total,
            Discount = orderDetails.Discount,
            FinalPrice = orderDetails.FinalPrice,
            ShippingId = orderDetails.ShippingId,
            ShippingAddress = orderDetails.ShippingAddress,
            ShippingPostalCode = orderDetails.ShippingPostalCode,
            ShippingMobile = orderDetails.ShippingMobile,
            OrderDetails = orderDetails.Details.Select(d => new Shop.Application.DTOs.OrderDetail.OrderDetailItem
                {
                    Id = d.Id,
                    OrderId = d.OrderId,
                    ProductId = d.ProductId,
                    ProductName = d.ProductName,
                    ProductImage = d.ProductImage,
                    Count = d.Count,
                    Price = d.Price,
                    Discount = d.Discount,
                    Total = d.Total
                })
                .ToList()
        };

        Response.Object = OrderDetailShowDto;
        return new JsonResult(Response);
    }

    #endregion
}