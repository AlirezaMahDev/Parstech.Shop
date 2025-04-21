using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop.Application.Features.Categury.Requests.Queries;

namespace Shop.Web.ViewComponents
{
    [ViewComponent(Name = "Categuries")]
    public class CateguriesViewComponent : ViewComponent
    {

        private readonly IMediator _mediator;
        public CateguriesViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var List = await _mediator.Send(new ShowCateguriesQueryReq());
            //var j=JsonConvert.SerializeObject(List);
            return View(List);
        }
    }
}
