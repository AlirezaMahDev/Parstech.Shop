using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.DTOs.Product;
using Shop.Application.Features.Product.Requests.Queries;
using Shop.Application.Features.ProductStockPrice.Requests.Queries;

namespace Shop.Web.Pages.Components
{
    public class GetProductStockPricesSelectModel : PageModel
    {
        #region Constractor
        private readonly IMediator _mediator;

        public GetProductStockPricesSelectModel(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        public List<ProductSelectDto> list { get; set; }
        public async Task OnGet(int repId)
        {

            list = await _mediator.Send(new ProductStockPriceSelectListQueryReq(repId));
        }
    }
}
