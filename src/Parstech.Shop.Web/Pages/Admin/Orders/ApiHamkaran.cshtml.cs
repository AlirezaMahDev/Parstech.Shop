using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Web.Services;

namespace Parstech.Shop.Web.Pages.Admin.Orders;

[Authorize(Roles = "SupperUser,Sale,Store,Finanicial")]
public class ApiHamkaranModel : PageModel
{
    #region Constructor

    private readonly IOrderAdminGrpcClient _orderAdminGrpcClient;

    public ApiHamkaranModel(IOrderAdminGrpcClient orderAdminGrpcClient)
    {
        _orderAdminGrpcClient = orderAdminGrpcClient;
    }

    #endregion

    #region Properties

    [BindProperty]
    public int OrderId { get; set; }

    [BindProperty]
    public RahkaranOrderDto OrderDto { get; set; } = new();

    [BindProperty]
    public RahkaranUserDto UserDto { get; set; } = new();

    [BindProperty]
    public RahkaranProductDto ProductDto { get; set; } = new();

    [BindProperty]
    public ResponseDto Response { get; set; } = new();

    #endregion

    #region Get

    public IActionResult OnGet(int id)
    {
        OrderId = id;
        return Page();
    }

    #endregion

    #region Rahkaran Order Operations

    public async Task<IActionResult> OnPostGetRahkaranOrder()
    {
        try
        {
            var rahkaranOrder = await _orderAdminGrpcClient.GetRahkaranOrderAsync(OrderId);

            Response.IsSuccessed = true;
            Response.Object = rahkaranOrder;

            return new JsonResult(Response);
        }
        catch (Exception ex)
        {
            Response.IsSuccessed = false;
            Response.Message = $"Error getting Rahkaran order: {ex.Message}";
            return new JsonResult(Response);
        }
    }

    public async Task<IActionResult> OnPostCreateRahkaranOrder()
    {
        try
        {
            var response = await _orderAdminGrpcClient.CreateRahkaranOrderAsync(OrderDto);

            Response.IsSuccessed = response.IsSuccessed;
            Response.Message = response.Message;
            Response.Code = response.Code;
            Response.Object = response.Object;

            return new JsonResult(Response);
        }
        catch (Exception ex)
        {
            Response.IsSuccessed = false;
            Response.Message = $"Error creating Rahkaran order: {ex.Message}";
            return new JsonResult(Response);
        }
    }

    #endregion

    #region Rahkaran User Operations

    public async Task<IActionResult> OnPostGetRahkaranUser(int userId)
    {
        try
        {
            var rahkaranUser = await _orderAdminGrpcClient.GetRahkaranUserAsync(userId);

            Response.IsSuccessed = true;
            Response.Object = rahkaranUser;

            return new JsonResult(Response);
        }
        catch (Exception ex)
        {
            Response.IsSuccessed = false;
            Response.Message = $"Error getting Rahkaran user: {ex.Message}";
            return new JsonResult(Response);
        }
    }

    public async Task<IActionResult> OnPostCreateRahkaranUser()
    {
        try
        {
            var response = await _orderAdminGrpcClient.CreateRahkaranUserAsync(UserDto);

            Response.IsSuccessed = response.IsSuccessed;
            Response.Message = response.Message;
            Response.Code = response.Code;
            Response.Object = response.Object;

            return new JsonResult(Response);
        }
        catch (Exception ex)
        {
            Response.IsSuccessed = false;
            Response.Message = $"Error creating Rahkaran user: {ex.Message}";
            return new JsonResult(Response);
        }
    }

    #endregion

    #region Rahkaran Product Operations

    public async Task<IActionResult> OnPostGetRahkaranProduct(int productId)
    {
        try
        {
            var rahkaranProduct = await _orderAdminGrpcClient.GetRahkaranProductAsync(productId);

            Response.IsSuccessed = true;
            Response.Object = rahkaranProduct;

            return new JsonResult(Response);
        }
        catch (Exception ex)
        {
            Response.IsSuccessed = false;
            Response.Message = $"Error getting Rahkaran product: {ex.Message}";
            return new JsonResult(Response);
        }
    }

    public async Task<IActionResult> OnPostCreateRahkaranProduct()
    {
        try
        {
            var response = await _orderAdminGrpcClient.CreateRahkaranProductAsync(ProductDto);

            Response.IsSuccessed = response.IsSuccessed;
            Response.Message = response.Message;
            Response.Code = response.Code;
            Response.Object = response.Object;

            return new JsonResult(Response);
        }
        catch (Exception ex)
        {
            Response.IsSuccessed = false;
            Response.Message = $"Error creating Rahkaran product: {ex.Message}";
            return new JsonResult(Response);
        }
    }

    #endregion

    #region Rahkaran Integration Operations

    public async Task<IActionResult> OnPostSendOrderToRahkaran()
    {
        try
        {
            var response = await _orderAdminGrpcClient.SendOrderToRahkaranAsync(OrderId);

            Response.IsSuccessed = response.IsSuccessed;
            Response.Message = response.Message;
            Response.Code = response.Code;
            Response.Object = response.Object;

            return new JsonResult(Response);
        }
        catch (Exception ex)
        {
            Response.IsSuccessed = false;
            Response.Message = $"Error sending order to Rahkaran: {ex.Message}";
            return new JsonResult(Response);
        }
    }

    public async Task<IActionResult> OnPostFollowOrderFromRahkaran()
    {
        try
        {
            var response = await _orderAdminGrpcClient.FollowOrderFromRahkaranAsync(OrderId);

            Response.IsSuccessed = response.IsSuccessed;
            Response.Message = response.Message;
            Response.Code = response.Code;
            Response.Object = response.Object;

            return new JsonResult(Response);
        }
        catch (Exception ex)
        {
            Response.IsSuccessed = false;
            Response.Message = $"Error following order from Rahkaran: {ex.Message}";
            return new JsonResult(Response);
        }
    }

    #endregion
}