using MediatR;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Parstech.Shop.Context.Application.DTOs.Section;

using Parstech.Shop.Context.Application.Features.Section.Requests.Queries;

namespace Parstech.Shop.Web.Pages.Components;

public class GetDiscountSectionSelect : PageModel
{
    #region Constractor
    private readonly IMediator _mediator;

    public GetDiscountSectionSelect(IMediator mediator)
    {
        _mediator = mediator;
    }
    #endregion

    public List<SectionDto> list { get; set; }
    public async Task OnGet()
    {

        list = await _mediator.Send(new DiscountSectionsSelectQueryReq());
    }
}