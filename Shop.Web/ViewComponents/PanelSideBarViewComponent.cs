using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Features.User.Requests.Queries;
using Shop.Application.Features.Wallet.Requests.Queries;

namespace Shop.Web.ViewComponents
{
    [ViewComponent(Name = "PanelSideBar")]
    public class PanelSideBarViewComponent : ViewComponent
    {

        private readonly IMediator _mediator;
        public PanelSideBarViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var CurrentUser = await _mediator.Send(new UserReadByUserNameQueryReq(User.Identity.Name));
            var walletDto = await _mediator.Send(new GetWalletByUserIdQueryReq(CurrentUser.Id));
            return View(walletDto);
        }
    }
}
