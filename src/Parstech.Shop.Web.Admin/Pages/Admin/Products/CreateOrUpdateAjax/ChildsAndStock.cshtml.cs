using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Product;
using Parstech.Shop.Context.Application.Features.Product.Requests.Commands;
using Parstech.Shop.Context.Application.Features.Product.Requests.Queries;
using Parstech.Shop.Context.Application.Features.User.Requests.Queries;
using Parstech.Shop.Context.Application.Features.UserStore.Requests.Queries;

namespace Parstech.Shop.Web.Admin.Pages.Admin.Products.CreateOrUpdateAjax;

public class ChildsAndStockModel : PageModel
{
    #region Constractor

    private readonly IMediator _mediator;

    private readonly IProductStockPriceRepository _productStockRep;


    public ChildsAndStockModel(IMediator mediator,

        IProductStockPriceRepository productStockRep)
    {
        _mediator = mediator;

        _productStockRep = productStockRep;
    }

    #endregion
    //id
    [BindProperty] public int? productId { get; set; }
    [BindProperty] public int? TypeId { get; set; } = 0;

    public ChildsAndStock ChildsAndStock { get; set; }
    public ProductDto product { get; set; }

    public async Task OnGet(int? id)
    {
        productId = id;
        if (productId != 0)
        {
            product = await _mediator.Send(new ProductReadCommandReq(id.Value));
            TypeId = product.TypeId;
        }
        if (User.IsInRole("Store") || User.IsInRole("StoreBySend"))
        {
            var user = await _mediator.Send(new UserReadByUserNameQueryReq(User.Identity.Name));
            var userStore = await _mediator.Send(new UserStoreOfUserReadQueryReq(user.Id));
            ChildsAndStock =
                await _mediator.Send(new GetChildsAndProductStocksQueryReq(productId.Value, userStore.Id));

                
        }
        else
        {
            ChildsAndStock =
                await _mediator.Send(new GetChildsAndProductStocksQueryReq(productId.Value, 0));
        }
    }
}