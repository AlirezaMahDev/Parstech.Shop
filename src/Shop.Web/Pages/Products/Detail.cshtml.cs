using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Office.Interop.Word;
using Shop.Application.DTOs.Api;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.ProductCategury;
using Shop.Application.DTOs.ProductGallery;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.Api.Requests.Queries;
using Shop.Application.Features.Product.Requests.Queries;
using Shop.Application.Features.ProductGallery.Requests.Queries;

namespace Shop.Web.Pages.Products
{
    public class DetailModel : PageModel
    {
        #region Constractor

        private readonly IMediator _mediator;

        public DetailModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region Properties

        //result
        [BindProperty]
        public ResponseDto Response { get; set; } = new ResponseDto();


        [BindProperty]
        public ProductDetailShowDto Item { get; set; } = new ProductDetailShowDto();


        [BindProperty]
        public string ShortLink { get; set; }


        [BindProperty]
        public int StoreId { get; set; }


        
        public List<ProductGalleryDto> Galleries { get; set; }

         public List<ProductCateguryDto> Categuries { get; set; }
        public TorobDto Torob { get; set; }
        #endregion

        #region Get

        public async Task<IActionResult> OnGet(string shortLink, int storeId)
        {
            ShortLink = shortLink;
           
            var product = await _mediator.Send(new GetProductByShortLinkQueryReq(shortLink));
            if (storeId == 0)
            {
                StoreId = product.BestStockId.Value;
            }
            else {
                StoreId = storeId;
            }
            
            Galleries = await _mediator.Send(new GalleriesOfProductQueryReq(product.Id));
            Categuries = await _mediator.Send(new CateguriesOfProductQueryReq(product.Id));
            
            #region Torob
            string baseUrl = $"{Request.Scheme}://{Request.Host.ToUriComponent()}";
            Torob = await _mediator.Send(new TorobGetProductQueryReq(storeId, baseUrl));
            #endregion

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
            Item = await _mediator.Send(new ProductDetailShowQueryReq(ShortLink, StoreId, userName));
            //Response.Object = Item;
            //Response.IsSuccessed = true;
            //return new JsonResult(Response);
            return Page();
        }

        public async Task<IActionResult> OnPostData()
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
            var Item = await _mediator.Send(new ProductDetailShowQueryReq(ShortLink, StoreId,userName));
            Response.Object = Item;
            Response.IsSuccessed = true;
            return new JsonResult(Response);
        }

        #endregion
    }
}
