using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.ProductProperty;
using Parstech.Shop.Context.Application.Features.ProductProperty.Requests.Queries;

namespace Parstech.Shop.Web.Admin.Pages.Admin.Products.CreateOrUpdateAjax;

public class FeuturesModel : PageModel
{
    #region Constractor

    private readonly IMediator _mediator;
        
    private readonly IProductStockPriceRepository _productStockRep;


    public FeuturesModel(IMediator mediator,
           
        IProductStockPriceRepository productStockRep)
    {
        _mediator = mediator;
           
        _productStockRep = productStockRep;
    }

    #endregion
    //id
    [BindProperty] public int? productId { get; set; }

    [BindProperty]
    public List<ProductPropertyDto> PropertyDtos { get; set; }

    public async Task OnGet(int? id)
    {
        productId = id;
        if (productId != null)
        {
            PropertyDtos = await _mediator.Send(new PropertiesOfProductQueryReq(productId.Value));
        }
    }
}