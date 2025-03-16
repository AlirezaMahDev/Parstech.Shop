using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.DTOs.IRole;
using Parstech.Shop.Web.Services.GrpcClients;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Web.Pages.Admin.Users
{
    [Authorize(Roles = "SupperUser")]
    public class PermissionModel : PageModel
    {
        #region Constructor

        private readonly IRoleAdminGrpcClient _roleAdminClient;

        public PermissionModel(IRoleAdminGrpcClient roleAdminClient)
        {
            _roleAdminClient = roleAdminClient;
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
            List = await _roleAdminClient.GetAllRolesAsync();
            return Page();
        }

        #endregion

        #region Add
        public async Task<IActionResult> OnPost()
        {
            await _roleAdminClient.CreateRoleAsync(Input);
            return Redirect("/Admin/Users/Permission");
        }

        #endregion
    }
}
