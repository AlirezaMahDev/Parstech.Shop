using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.DTOs.User;
using Shop.Application.Features.User.Requests.Queries;

namespace Shop.Web.ViewComponents
{
    [ViewComponent(Name = "UserName")]
    public class UserNameViewComponent : ViewComponent
    {
        private readonly IMediator _mediator;
        public UserNameViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IViewComponentResult> InvokeAsync(UserNameDto param)
        {
            var userInfo = await _mediator.Send(new GetUserInfoQueryReq(param.userName, param.Position));
            return View(userInfo);
        }
    }
}
