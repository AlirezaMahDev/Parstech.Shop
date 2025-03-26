using MediatR;

using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Context.Application.DTOs.Categury;
using Parstech.Shop.Context.Application.Features.Categury.Requests.Queries;

namespace Parstech.Shop.Web.Pages.Components;

public class GetCategurySelectModel : PageModel
{
    #region Constractor
    private readonly IMediator _mediator;

    public GetCategurySelectModel(IMediator mediator)
    {
        _mediator = mediator;
    }
    #endregion

    public List<CategurySelectDto> list { get; set; }
    public async Task OnGet()
    {

        list = await _mediator.Send(new CategurySelectListQueryReq());
    }
}