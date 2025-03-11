using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.DTOs.IRole;
using Shop.Application.Features.IRole.Requests.Commands;

namespace Shop.Web.Pages.Admin.Users
{
    [Authorize(Roles = "SupperUser")]
    public class PermissionModel : PageModel
    {
        #region Constractor

        private readonly IMediator _mediator;

        public PermissionModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region Properties

        [BindProperty]
        public IRoleDto Input { get; set; }


        [BindProperty]
        public List<IRoleDto> List { get; set; }

        #endregion

        #region Get

        public async Task<IActionResult> OnGet()
        {
            List = await _mediator.Send(new IRoleReadAllCommandReq());
            return Page();
        }

        #endregion

        #region Add
        public async Task<IActionResult> OnPost()
        {
            await _mediator.Send(new IRoleCreateCommandReq(Input));
            return Redirect("/Admin/Users/Permission");
        }


        #endregion

    }
}
