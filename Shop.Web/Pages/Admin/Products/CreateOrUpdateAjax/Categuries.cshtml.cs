using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.ProductCategury;
using Shop.Application.DTOs.ProductGallery;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.Categury.Requests.Commands;
using Shop.Application.Features.Product.Requests.Commands;
using Shop.Application.Features.Product.Requests.Queries;
using Shop.Application.Features.ProductCategury.Requests.Commands;
using Shop.Application.Features.ProductGallery.Requests.Queries;
using Shop.Application.Features.ProductStockPrice.Requests.Commands;
using Shop.Application.Features.ProductStockPrice.Requests.Queries;
using Shop.Domain.Models;
using System.Threading.Tasks;


namespace Shop.Web.Pages.Admin.Products.CreateOrUpdateAjax
{
    public class CateguriesModel : PageModel
    {
        #region Constractor

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IProductStockPriceRepository _productStockRep;


        public CateguriesModel(IMediator mediator,
            IMapper mapper,
            IProductStockPriceRepository productStockRep)
        {
            _mediator = mediator;
            _mapper = mapper;
            _productStockRep = productStockRep;
        }

        #endregion
        //id
        [BindProperty] public int? productId { get; set; }


        //product
        [BindProperty] public List<ProductCateguryDto> Categuries { get; set; }

        [BindProperty] public ProductCateguryDto categury { get; set; }

        [BindProperty] public List<ProductCateguryDto> categuries { get; set; }

        //result
        [BindProperty] public ResponseDto Response { get; set; } = new ResponseDto();

        [BindProperty] public string FilterCat { get; set; }

        public async Task OnGet(int? id)
        {
            productId = id;
            if (productId != null)
            {
                Categuries = await _mediator.Send(new CateguriesOfProductQueryReq(productId.Value));
            }
        }



        #region Categury

        public async Task<IActionResult> OnPostGetAllCateguries()
        {
            var categuries = await _mediator.Send(new CateguryReadCommandReq(FilterCat));
            Response.Object = categuries;
            Response.IsSuccessed = true;
            return new JsonResult(Response);
        }

        public async Task<IActionResult> OnPostCateguries()
        {
            categuries = await _mediator.Send(new CateguriesOfProductQueryReq(productId.Value));
            Response.Object = categuries;
            return new JsonResult(Response);
        }

        public async Task<IActionResult> OnPostCategury()
        {
            categury = await _mediator.Send(new CateguryOfProductQueryReq(productId.Value));
            Response.Object = categury;
            return new JsonResult(Response);
        }

        public async Task<IActionResult> OnPostDeleteCategury()
        {
            var ProductId = await _mediator.Send(new ProductCateguryDeleteCommandReq(productId.Value));
            Response.Object = ProductId;
            Response.IsSuccessed = true;
            return new JsonResult(Response);
        }


        

        #endregion
    }
}
