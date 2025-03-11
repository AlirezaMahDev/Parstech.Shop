using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.DTOs.Product;
using Shop.Application.Features.Product.Requests.Queries;

namespace Shop.Web.Pages.Components
{
    public class GetProductsSelectModel : PageModel
    {
        #region Constractor
        private readonly IMediator _mediator;

        public GetProductsSelectModel(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        public List<ProductSelectDto> list { get; set; }
        public async Task OnGet()
        {

            list = await _mediator.Send(new ProductSelectListQueryReq());
        }
    }
}
