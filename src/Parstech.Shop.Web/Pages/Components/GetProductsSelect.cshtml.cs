using MediatR;

using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Context.Application.DTOs.Product;
using Parstech.Shop.Context.Application.Features.Product.Requests.Queries;

namespace Parstech.Shop.Web.Pages.Components;

public class GetProductsSelectModel : PageModel
{
    #region Constractor
    private readonly IMediator _mediator;

    public GetProductsSelectModel(IMediator mediator)
    {
        _mediator = mediator;
    }
    #endregion

    public List<ProductSelectDto> list { get; set; }
    public async Task OnGet()
    {

        list = await _mediator.Send(new ProductSelectListQueryReq());
    }
}