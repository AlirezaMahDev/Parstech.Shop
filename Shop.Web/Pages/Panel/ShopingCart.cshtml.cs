using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.DTOs.Order;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.Order.Requests.Queries;

namespace Shop.Web.Pages.Panel
{
    [Authorize]
    public class ShopingCartModel : PageModel
    {
        #region Constractor

        private readonly IMediator _mediator;
        public ShopingCartModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region Properties
        [BindProperty]
        public OrderDetailShowDto ShoppingCart { get; set; }

        [BindProperty]
        public ResponseDto Response { get; set; } = new ResponseDto();

        [BindProperty]
        public int UserId { get; set; }
        #endregion

        #region Get
        public async Task<IActionResult> OnGet()
        {

            return Page();
        }
        public async Task<IActionResult> OnPostData()
        {
            ShoppingCart = await _mediator.Send(new NotFinallyOrderOfUserQueryReq(UserId));
            Response.Object = ShoppingCart;
            Response.IsSuccessed = true;

            return new JsonResult(Response);
        }
        #endregion
    }
}
