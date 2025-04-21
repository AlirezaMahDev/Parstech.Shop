using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.DTOs.Categury;
using Shop.Application.DTOs.Section;
using Shop.Application.Features.Categury.Requests.Queries;
using Shop.Application.Features.Section.Requests.Queries;
using System.Drawing;

namespace Shop.Web.Pages.Components
{
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
}
