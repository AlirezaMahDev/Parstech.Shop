using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.DTOs.Categury;
using Shop.Application.DTOs.Product;
using Shop.Application.Features.Categury.Requests.Queries;
using Shop.Application.Features.Product.Requests.Queries;

namespace Shop.Web.Pages.Components
{
    public class GetCategurySelectModel : PageModel
    {
        #region Constractor
        private readonly IMediator _mediator;

        public GetCategurySelectModel(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        public List<CategurySelectDto> list { get; set; }
        public async Task OnGet()
        {

            list = await _mediator.Send(new CategurySelectListQueryReq());
        }
    }
}
