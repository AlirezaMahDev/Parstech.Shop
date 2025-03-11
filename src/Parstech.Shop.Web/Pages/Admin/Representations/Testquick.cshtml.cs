using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.ProductRepresentation;
using Shop.Application.DTOs.Representation;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.ProductRepresentation.Requests.Queries;
using Shop.Application.Features.ProductStockPrice.Requests.Queries;
using Shop.Application.Features.Representation.Requests.Commands;
using Shop.Application.Features.User.Requests.Queries;
using Shop.Application.Features.UserStore.Requests.Queries;

namespace Shop.Web.Pages.Admin.Representations
{
    public class TestquickModel : PageModel
    {
        #region Constractor 
        private readonly IMediator _mediator;
        private readonly IProductRepresesntationRepository _productRepresesntationRep;

        public TestquickModel(IMediator mediator, IProductRepresesntationRepository productRepresesntationRep)
        {
            _mediator = mediator;
            _productRepresesntationRep = productRepresesntationRep;
        }
        #endregion

        #region
        public ProductRepresentationPagingDto list { get; set; }

        [BindProperty]
        public ProductRepresenationParameterDto parameters { get; set; } = new ProductRepresenationParameterDto();
        public ResponseDto response { get; set; } = new ResponseDto();
        #endregion

        #region Get

        public async Task<IActionResult> OnGet()
        {
            return Page();
        }



        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostGetData()
        {
            parameters.TakePage = 30;
            if (parameters.CurrentPage == 0)
            {
                parameters.CurrentPage = 1;
            }

            var user = await _mediator.Send(new UserReadByUserNameQueryReq(User.Identity.Name));
            var userStore = await _mediator.Send(new UserStoreOfUserReadQueryReq(user.Id));
            if (userStore == null)
            {
                response.Object = null;
                response.IsSuccessed = false;
                return new JsonResult(response);
            }

            var rep = await _mediator.Send(new RepresentationReadCommandReq(userStore.RepId));
            parameters.RepId = rep.Id;
            list = await _mediator.Send(new ProductRepresentaionPagingQueryReq(parameters));
            response.Object = list;
            response.IsSuccessed = true;
            return new JsonResult(response);
        }
        #endregion
        #region SaveChanges
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostSaveChanges([FromBody] List<QuickEditDto> list)
        {

            var response = await _mediator.Send(new QuickEditProductStockPricesQueryReq(User.Identity.Name, list));
            return new JsonResult(response);
        }
        #endregion
    }
}
