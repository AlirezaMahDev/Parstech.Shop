using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Web.Services;

namespace Parstech.Shop.Web.Pages.Checkout;

public class PaymentModel : PageModel
{
    private readonly PaymentGrpcClient _paymentClient;

    public PaymentModel(PaymentGrpcClient paymentClient)
    {
        _paymentClient = paymentClient;
    }

    public ResponseDto Result { get; set; } = new();

    public async Task<IActionResult> OnGet(string Status)
    {
        var response = await _paymentClient.GetPaymentStatusAsync(Status);

        Result.IsSuccessed = response.IsSuccessed;
        Result.Message = response.Message;

        return Page();
    }
}