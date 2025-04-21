using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Features.SiteSeting.Requests.Queries;

namespace Shop.Web.ViewComponents
{
    [ViewComponent(Name = "Header")]
    public class HeaderViewComponent : ViewComponent
    {

        private readonly IMediator _mediator;
        public HeaderViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _mediator.Send(new GetSettingAndSeoQueryReq());
            return View(result);
        }
    }
}
