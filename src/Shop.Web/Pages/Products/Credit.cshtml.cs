using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Charts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Shop.Application.DTOs.Api;
using Shop.Application.DTOs.CreditProductStockPrice;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.ProductCategury;
using Shop.Application.DTOs.ProductGallery;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.Api.Requests.Queries;
using Shop.Application.Features.CreditProductStockPrice.Requests.Queries;
using Shop.Application.Features.Order.Requests.Queries;
using Shop.Application.Features.OrderShipping.Request.Queries;
using Shop.Application.Features.Product.Requests.Queries;
using Shop.Application.Features.ProductGallery.Requests.Queries;
using Shop.Domain.Models;

namespace Shop.Web.Pages.Products
{
    public class CreditModel : PageModel
    {
        #region Constractor

        private readonly IMediator _mediator;

        public CreditModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region Properties

        //result
        [BindProperty]
        public ResponseDto Response { get; set; } = new ResponseDto();


        
        public List<CreditProductStockPriceDto>? Items { get; set; }


        [BindProperty]
        public CreditProductStockPriceDto? Credit { get; set; } 


        
        public string ShortLink { get; set; }

        #endregion

        #region Get

        public async Task<IActionResult> OnGet(string shortLink)
        {
            ShortLink = shortLink;
            #region Get User If Authenticated
            var userName = "";
            if (User.Identity.IsAuthenticated)
            {
                userName = User.Identity.Name;
            }
            else
            {
                userName = null;
            }
            #endregion
            Items = await _mediator.Send(new GetCreditProductStockPriceByProductShortlinkQueryReq(ShortLink));
            return Page();
        }
        public async Task<IActionResult> OnPostComplete()
        {
            var creditProduct = await _mediator.Send(new GetCreditProductStockPriceQueryReq(Credit.Id,Credit.ProductStockPriceId)); 
            var order = await _mediator.Send(new CreateCheckoutForCreditProductQueryReq(User.Identity.Name, creditProduct)); 
            
            if(order.OrderId==0 || order.OrderId == null)
            {
                ResponseDto response = new ResponseDto();
                response.IsSuccessed = false;
                response.Message = "تا زمانی که اقساط اعتباری پرداخت نشده دارید امکان ثبت خرید جدید وجود ندارد";
                return new JsonResult(Response);
            }
            var shipping=await _mediator.Send(new OrderShippingChangeQueryReq("Change", 0, order.OrderId, 0));
            var payTypeId = 0;
            if (Credit.PrePay > 0)
            {
                payTypeId = 7;
            }
            else{
                payTypeId = 8;
            }
            var orrderShipping= await _mediator.Send(new OrderShippingGetByOrderIdQueryReq(order.OrderId));
            Response = await _mediator.Send(new CompleteOrderForCreditProductQueryReq(order.OrderId, orrderShipping.Id, payTypeId, creditProduct));
            return new JsonResult(Response);
        }



        #endregion
    }
}
