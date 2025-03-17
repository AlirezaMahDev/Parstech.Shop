using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Web.Services;

namespace Parstech.Shop.Web.Pages.Checkout;

[Authorize]
public class IndexModel : PageModel
{
    #region Constructor

    private readonly OrderCheckoutGrpcClient _orderCheckoutClient;
    private readonly CouponGrpcClient _couponClient;
    private readonly WalletGrpcClient _walletClient;
    private readonly ShippingGrpcClient _shippingClient;

    public IndexModel(
        OrderCheckoutGrpcClient orderCheckoutClient,
        CouponGrpcClient couponClient,
        WalletGrpcClient walletClient,
        ShippingGrpcClient shippingClient)
    {
        _orderCheckoutClient = orderCheckoutClient;
        _couponClient = couponClient;
        _walletClient = walletClient;
        _shippingClient = shippingClient;
    }

    #endregion

    #region Properties

    //result
    [BindProperty]
    public ResponseDto Response { get; set; } = new();

    public List<PayTypeDto> payTypes { get; set; }

    #endregion

    #region Get

    public IActionResult OnGet()
    {
        return Page();
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostData()
    {
        var order = await _orderCheckoutClient.GetOpenOrderAsync(User.Identity.Name);
        await _orderCheckoutClient.RefreshOrderAsync(order.OrderId);

        if (order.OrderId != 0)
        {
            try
            {
                var orderShipping = await _shippingClient.GetOrderShippingAsync(order.OrderId);

                if (orderShipping.Id == 0)
                {
                    var shipping = await _shippingClient.GetFirstUserShippingAsync(order.UserId);
                    await _shippingClient.ChangeOrderShippingAsync("Change", shipping.ShippingId, order.OrderId, 0);
                }
            }
            catch
            {
                // If shipping not found, try to get first user shipping
                var shipping = await _shippingClient.GetFirstUserShippingAsync(order.UserId);
                await _shippingClient.ChangeOrderShippingAsync("Change", shipping.ShippingId, order.OrderId, 0);
            }
        }

        var orderDetails = await _orderCheckoutClient.GetOrderDetailsAsync(order.OrderId);
        Response.IsSuccessed = true;
        Response.Object = orderDetails;

        return new JsonResult(Response);
    }

    [ValidateAntiForgeryToken]
    public async Task<JsonResult> OnPostGetWallet(int userId, int type)
    {
        ResponseDto Response = new();
        string typeName = null;

        if (User.Identity.IsAuthenticated)
        {
            var wallet = await _walletClient.GetWalletByUserIdAsync(userId);
            Response.Object = wallet;
            Response.IsSuccessed = true;

            switch (type)
            {
                case 3:
                    typeName = "Fecilities";
                    try
                    {
                        var activeTransaction =
                            await _walletClient.GetActiveTransactionAsync(wallet.WalletId, typeName);
                        Response.Object2 = activeTransaction;
                    }
                    catch
                    {
                        // No active transaction found
                    }

                    break;

                case 4:
                    typeName = "OrgCredit";
                    try
                    {
                        var activeTransaction =
                            await _walletClient.GetActiveTransactionAsync(wallet.WalletId, typeName);
                        Response.Object2 = activeTransaction;
                    }
                    catch
                    {
                        // No active transaction found
                    }

                    break;

                default:
                    break;
            }
        }

        return new(Response);
    }

    #endregion

    #region OrderDetail

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostChangeDetail(int detailId, int count)
    {
        var result = await _orderCheckoutClient.ChangeOrderDetailAsync(detailId, count);

        Response.IsSuccessed = result.Status;
        Response.Object = result;

        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostDelete(int id)
    {
        var result = await _orderCheckoutClient.DeleteOrderDetailAsync(id);

        Response.IsSuccessed = result.Status;
        Response.Message = result.Message;

        return new JsonResult(Response);
    }

    #endregion

    #region Coupon

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostUseCoupon(int orderId, string code)
    {
        var result = await _couponClient.UseCouponAsync(orderId, code);

        Response.IsSuccessed = result.Status;
        Response.Object = result;

        return new JsonResult(Response);
    }

    #endregion

    #region Shipping

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostChangeShipping(int userShippingId, int orderId)
    {
        var result = await _shippingClient.ChangeOrderShippingAsync("Change", userShippingId, orderId, 0);

        Response.IsSuccessed = true;
        Response.Message = "عملیات با موفقیت انجام شد";

        return new JsonResult(Response);
    }

    #endregion

    #region Complete

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostCalculateAghsat(long price, int transactionId, int month)
    {
        var result = await _walletClient.CalculateInstallmentsAsync(price, transactionId, month);

        Response.IsSuccessed = result.IsSuccessed;
        Response.Message = result.Message;
        Response.Object = new { result.MonthlyAmount, result.TotalAmount };

        return new JsonResult(Response);
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostComplete(int orderId,
        int orderShippingId,
        int payTypeId,
        int? transactionId,
        int month)
    {
        var result =
            await _orderCheckoutClient.CompleteOrderAsync(orderId, orderShippingId, payTypeId, transactionId, month);

        Response.IsSuccessed = result.IsSuccessed;
        Response.Message = result.Message;
        Response.Object = result.Result;

        return new JsonResult(Response);
    }

    #endregion
}