using AutoMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.ProductCategury;
using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.Features.Categury.Requests.Commands;
using Parstech.Shop.Context.Application.Features.ProductCategury.Requests.Commands;
using Parstech.Shop.Context.Application.Features.ProductCategury.Requests.Queries;

namespace Parstech.Shop.Web.Admin.Pages.Admin.Products.CreateOrUpdateAjax;

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
    [BindProperty] public ResponseDto Response { get; set; } = new();

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