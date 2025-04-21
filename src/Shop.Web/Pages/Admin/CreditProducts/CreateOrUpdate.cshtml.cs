using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.DTOs.CreditProductStockPrice;
using Shop.Application.Features.Api.Requests.Queries;
using Shop.Application.Features.CreditProductStockPrice.Requests.Queries;

namespace Shop.Web.Pages.Admin.CreditProducts
{
    public class CreateOrUpdateModel : PageModel
    {
        private readonly IMediator _mediator;

        public CreateOrUpdateModel(IMediator mediator)
        {
            _mediator = mediator;
        }
        public CreditProductStockPriceDto Item { get; set; }
        public async Task<IActionResult> OnGet(int productStockPriceId, int? id)
        {
            Item =await _mediator.Send(new GetCreditProductStockPriceQueryReq(id, productStockPriceId));
            return Page();
        }
    }
}
