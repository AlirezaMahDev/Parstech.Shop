using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.DTOs.Brand;
using Shop.Application.DTOs.Categury;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.Response;
using Shop.Application.DTOs.UserStore;
using Shop.Application.Features.Product.Requests.Queries;
using Shop.Application.Features.UserStore.Requests.Queries;

namespace Shop.Web.Pages.Products
{
    public class StoresModel : PageModel
    {
        #region Constractor

        private readonly IMediator _mediator;

        public StoresModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region Properties

        //paging parameter
        [BindProperty]
        public ProductSearchParameterDto Parameter { get; set; } = new ProductSearchParameterDto();


        //products
        [BindProperty]
        public ProductPageingDto List { get; set; }


        //result
        [BindProperty]
        public ResponseDto Response { get; set; } = new ResponseDto();

        //categury
        [BindProperty]
        public string Store { get; set; }



        [BindProperty]
        public string FilterCat { get; set; }

        [BindProperty]
        public int Type { get; set; }

        [BindProperty]
        public string Filter { get; set; }


        public List<CateguryDto> categuries { get; set; }
        public List<BrandDto> Brands { get; set; }
        public List<UserStoreDto> Stores { get; set; }

        #endregion

        #region Get

        public async Task<IActionResult> OnGet(string store)
        {
            Store = store;

            return Page();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostData(int skip, string store)
        {
            //Parameter.CurrentPage = 1;
            Parameter.Skip = skip;
            Parameter.Store = store;
            List = await _mediator.Send(new ProductPagingSarachOrStoreQueryReq(Parameter));
            Response.Object = List;
            var userStore = await _mediator.Send(new UserSaleReadByLatinNameQueryReq(Parameter.Store));
            Response.Object2 = userStore;
            Response.IsSuccessed = true;
            return new JsonResult(Response);
        }

        #endregion
    }
}
