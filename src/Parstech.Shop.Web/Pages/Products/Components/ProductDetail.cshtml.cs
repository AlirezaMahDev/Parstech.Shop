using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.Product.Requests.Queries;

namespace Shop.Web.Pages.Products.Components
{
    public class ProductDetailModel : PageModel
    {
        #region Constractor

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IProductStockPriceRepository _productStockRep;

        public ProductDetailModel(IMediator mediator,
            IMapper mapper,
            IProductStockPriceRepository productStockRep)
        {
            _mediator = mediator;
            _mapper = mapper;
            _productStockRep = productStockRep;
        }

        #endregion
        [BindProperty] public ResponseDto Response { get; set; } = new ResponseDto();
        

        public async Task<IActionResult> OnGet(string ShortLink, int StoreId)
        {
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
            var Item = await _mediator.Send(new ProductDetailShowQueryReq(ShortLink, StoreId, userName));
            Response.Object = Item;
            Response.IsSuccessed = true;
            return new JsonResult(Response);
        }
    }
}
