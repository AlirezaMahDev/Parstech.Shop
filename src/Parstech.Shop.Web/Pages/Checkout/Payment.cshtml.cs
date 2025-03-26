using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Response;

namespace Parstech.Shop.Web.Pages.Checkout;

public class PaymentModel : PageModel
{
    private readonly IWalletTransactionRepository _walletTransaction;
    private readonly IMediator _mediator;
    public PaymentModel(IMediator mediator, IWalletTransactionRepository walletTransaction)
    {
        _mediator = mediator;
        _walletTransaction = walletTransaction;
    }

    public ResponseDto Result { get; set; } = new();

    public async Task<IActionResult> OnGet(string Status)
    {

        if (Status == "Ok")
        {
            Result.IsSuccessed = true;
            Result.Message = "عملیات پرداخت موفق است";
        }
        else
        {
            Result.IsSuccessed = false;
            Result.Message = "عملیات پرداخت ناموفق است";
        }
        //if (HttpContext.Request.Query["Status"]!=""&&
        //    HttpContext.Request.Query["Status"].ToString().ToLower()=="ok"&&
        //    HttpContext.Request.Query["Authority"]!="")
        //{
        //    string authority = HttpContext.Request.Query["Authority"].ToString();
        //    var order =await _mediator.Send(new OrderReadCommandReq(Id));
        //    var payment = new ZarinpalSandbox.Payment(Convert.ToInt32(order.Total));
        //    var res = payment.Verification(authority).Result;
        //    if (res.Status == 100)
        //    {
        //        Result = await _mediator.Send(new CallBackCompleteOrderQueryReq(order.OrderId, TransactionId, res.RefId.ToString()));

        //    }
        //    else
        //    {
        //        Result.IsSuccessed = false;
        //        Result.Message = "عملیات پرداخت ناموفق است";
        //    }
        //}
        //else
        //{

        //        Result.IsSuccessed = false;
        //        Result.Message = "عملیات پرداخت ناموفق است";

        //}
        return Page();
    }




    //public async Task<IActionResult> OnGet(int Id,int TransactionId)
    //{
    //    var transaction =await _walletTransaction.GetAsync(TransactionId);
    //    if (transaction.TypeId == 4)
    //    {
    //        Result.IsSuccessed = true;
    //        Result.Message = "عملیات پرداخت موفق است";
    //    }
    //    else
    //    {
    //        Result.IsSuccessed = false;
    //        Result.Message = "عملیات پرداخت ناموفق است";
    //    }
    //    //if (HttpContext.Request.Query["Status"]!=""&&
    //    //    HttpContext.Request.Query["Status"].ToString().ToLower()=="ok"&&
    //    //    HttpContext.Request.Query["Authority"]!="")
    //    //{
    //    //    string authority = HttpContext.Request.Query["Authority"].ToString();
    //    //    var order =await _mediator.Send(new OrderReadCommandReq(Id));
    //    //    var payment = new ZarinpalSandbox.Payment(Convert.ToInt32(order.Total));
    //    //    var res = payment.Verification(authority).Result;
    //    //    if (res.Status == 100)
    //    //    {
    //    //        Result = await _mediator.Send(new CallBackCompleteOrderQueryReq(order.OrderId, TransactionId, res.RefId.ToString()));

    //    //    }
    //    //    else
    //    //    {
    //    //        Result.IsSuccessed = false;
    //    //        Result.Message = "عملیات پرداخت ناموفق است";
    //    //    }
    //    //}
    //    //else
    //    //{

    //    //        Result.IsSuccessed = false;
    //    //        Result.Message = "عملیات پرداخت ناموفق است";

    //    //}
    //    return Page();
    //}

}