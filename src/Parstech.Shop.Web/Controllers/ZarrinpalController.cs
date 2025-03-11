using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.Order.Requests.Queries;

namespace Shop.Web.Controllers
{


    public class ZarrinpalController : Controller
    {



        private readonly IMediator _mediator;
        private readonly IOrderRepository _orderRep;
        private readonly IWalletTransactionRepository _walletTransactionRep;

        public ZarrinpalController(IMediator mediator,
            IOrderRepository orderRep, IWalletTransactionRepository walletTransactionRep)
        {
            _mediator = mediator;
            _orderRep = orderRep;
            _walletTransactionRep = walletTransactionRep;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> CallBack(int orderId)
        {
            ResponseDto Result = new ResponseDto();
            var order = await _orderRep.GetAsync(orderId);
            var transaaction = await _walletTransactionRep.GetLastOfOrder(order.OrderCode);
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" &&
                HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"].ToString();

                var payment = new ZarinpalSandbox.Payment(Convert.ToInt32(order.Total));
                var res = payment.Verification(authority).Result;
                if (res.Status == 100)
                {
                    Result = await _mediator.Send(new CallBackCompleteOrderQueryReq(order.OrderId, transaaction.Id, res.RefId.ToString()));
                    var redirect = $"https://localhost:7040/checkout/payment/Ok";
                    return Redirect(redirect);
                }
                else
                {
                    var redirect = $"https://localhost:7040/checkout/payment/NotOk";
                    return Redirect(redirect);
                }
            }
            else
            {

                var redirect = $"https://localhost:7040/checkout/payment/NotOk";
                return Redirect(redirect);

            }

        }
    }
}
