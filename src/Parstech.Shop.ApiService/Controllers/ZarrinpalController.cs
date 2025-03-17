using Microsoft.AspNetCore.Mvc;

namespace Parstech.Shop.ApiService.Controllers;

public class ZarrinpalController : Controller
{
    private readonly OrderCheckoutGrpcClient _orderCheckoutGrpcClient;
    private readonly WalletTransactionGrpcClient _walletTransactionGrpcClient;

    public ZarrinpalController(
        OrderCheckoutGrpcClient orderCheckoutGrpcClient,
        WalletTransactionGrpcClient walletTransactionGrpcClient)
    {
        _orderCheckoutGrpcClient = orderCheckoutGrpcClient;
        _walletTransactionGrpcClient = walletTransactionGrpcClient;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> CallBack(string Authority, string Status)
    {
        if (Status != "OK")
        {
            return RedirectToAction("PaymentFailed", "Order");
        }

        var transaction = await _walletTransactionGrpcClient.GetWalletTransactionByTokenAsync(Authority);
        if (!transaction.Status || transaction.Transaction == null)
        {
            return RedirectToAction("PaymentFailed", "Order");
        }

        var transactionItem = transaction.Transaction;
        var orderId = transactionItem.OrderId;

        if (orderId > 0)
        {
            var orderDetails = await _orderCheckoutGrpcClient.GetOrderDetailsAsync(orderId);
            if (!orderDetails.OrderId.Equals(orderId))
            {
                return RedirectToAction("PaymentFailed", "Order");
            }

            var result = await _orderCheckoutGrpcClient.CompleteOrderAsync(
                orderId,
                orderDetails.ShippingId,
                1, // Payment type ID for Zarrinpal
                transactionItem.Id,
                transactionItem.TrackingCode);

            if (result.IsSuccessed)
            {
                await _walletTransactionGrpcClient.UpdateWalletTransactionAsync(
                    transactionItem.Id,
                    true,
                    transactionItem.TrackingCode);

                return RedirectToAction("PaymentSuccess", "Order", new { trackingCode = result.Result?.TrackingCode });
            }
        }

        return RedirectToAction("PaymentFailed", "Order");
    }
}