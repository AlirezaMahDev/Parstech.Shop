using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.Web.Services.GrpcClients;

namespace Parstech.Shop.Web.Pages.Admin.Users;

[Authorize(Roles = "SupperUser,Inventory,Finanicial,Sale")]
public class IndexModel : PageModel
{
    #region Constructor

    private readonly IUserAdminGrpcClient _userAdminClient;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;


    public IndexModel(
        IUserAdminGrpcClient userAdminClient,
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager)
    {
        _userAdminClient = userAdminClient;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    #endregion

    #region Properties

    [BindProperty]
    public UserParameterDto Parameter { get; set; } = new UserParameterDto();

    [BindProperty]
    public UserPageingDto List { get; set; }

    [BindProperty]
    public CreateUserDto CreateUserDto { get; set; }

    [BindProperty]
    public UpdateUserDto UpdateUserDto { get; set; }

    [BindProperty]
    public UserDto UserDto { get; set; }

    [BindProperty]
    public int userId { get; set; }

    [BindProperty]
    public UserRoleDto UserRoleDto { get; set; }

    // Billing properties
    [BindProperty]
    public CreateUserBillingDto CreateUserBillingDto { get; set; }

    [BindProperty]
    public UpdateUserBillingDto UpdateUserBillingDto { get; set; }

    [BindProperty]
    public UserBillingDto UserBillingDto { get; set; }

    [BindProperty]
    public int billingId { get; set; }

    // Shipping properties
    [BindProperty]
    public CreateUserShippingDto CreateUserShippingDto { get; set; }

    [BindProperty]
    public UpdateUserShippingDto UpdateUserShippingDto { get; set; }

    [BindProperty]
    public UserShippingDto UserShippingDto { get; set; }

    [BindProperty]
    public int shippingId { get; set; }

    [BindProperty]
    public ResponseDto Response { get; set; } = new ResponseDto();

    #endregion

    #region Get

    public IActionResult OnGet()
    {
        return Page();
    }

    #endregion

    #region GetData

    public async Task<IActionResult> OnPostGetData()
    {
        List = await _userAdminClient.GetUsersAsync(Parameter);
        Response.Object = List;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    #endregion

    #region Search

    public async Task<IActionResult> OnPostSearch()
    {
        List = await _userAdminClient.GetUsersAsync(Parameter);
        Response.Object = List;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    #endregion

    #region Paging

    public async Task<IActionResult> OnPostPaging()
    {
        Parameter.Take = 30;
        List = await _userAdminClient.GetUsersAsync(Parameter);
        Response.Object = List;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    #endregion

    #region Get User

    public async Task<IActionResult> OnPostGetUser()
    {
        var result = await _userAdminClient.GetUserAsync(userId);
        if (result.Status)
        {
            UserDto = result.User;
            Response.Object = UserDto;
            Response.IsSuccessed = true;
        }
        else
        {
            Response.IsSuccessed = false;
            Response.Message = result.Message;
        }

        return new JsonResult(Response);
    }

    #endregion

    #region Create Or Update

    public async Task<IActionResult> OnPostCreateOrUpdate()
    {
        if (UpdateUserDto != null && UpdateUserDto.Id != 0)
        {
            var result = await _userAdminClient.UpdateUserAsync(UpdateUserDto);
            Response.IsSuccessed = result.IsSuccessed;
            Response.Message = result.Message;
        }
        else if (CreateUserDto != null)
        {
            var result = await _userAdminClient.CreateUserAsync(CreateUserDto);
            Response.IsSuccessed = result.IsSuccessed;
            Response.Message = result.Message;
        }

        return new JsonResult(Response);
    }

    #endregion

    #region Login Bypass

    public async Task<IActionResult> OnPostLoginByUser()
    {
        var result = await _userAdminClient.LoginByUserAsync(userId);
        if (result.IsSuccessed)
        {
            Response.IsSuccessed = true;
            Response.Object = result.Object;
        }
        else
        {
            Response.IsSuccessed = false;
            Response.Message = result.Message;
        }

        return new JsonResult(Response);
    }

    #endregion

    #region User Roles

    public async Task<IActionResult> OnPostGetUserRoles()
    {
        var result = await _userAdminClient.GetUserRolesAsync(userId);
        if (result.IsSuccessed)
        {
            Response.Object = result.Object;
            Response.IsSuccessed = true;
        }
        else
        {
            Response.IsSuccessed = false;
            Response.Message = result.Message;
        }

        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostAddUserRole()
    {
        var result = await _userAdminClient.AddUserRoleAsync(userId, UserRoleDto);
        Response.IsSuccessed = result.IsSuccessed;
        Response.Message = result.Message;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostDeleteUserRole()
    {
        var result = await _userAdminClient.DeleteUserRoleAsync(userId, UserRoleDto);
        Response.IsSuccessed = result.IsSuccessed;
        Response.Message = result.Message;
        return new JsonResult(Response);
    }

    #endregion

    #region User Billings

    public async Task<IActionResult> OnPostGetUserBillings()
    {
        var result = await _userAdminClient.GetUserBillingsAsync(userId);
        if (result.IsSuccessed)
        {
            Response.Object = result.Object;
            Response.IsSuccessed = true;
        }
        else
        {
            Response.IsSuccessed = false;
            Response.Message = result.Message;
        }

        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostGetUserBilling()
    {
        var result = await _userAdminClient.GetUserBillingAsync(billingId);
        if (result.IsSuccessed)
        {
            Response.Object = result.Object;
            Response.IsSuccessed = true;
        }
        else
        {
            Response.IsSuccessed = false;
            Response.Message = result.Message;
        }

        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostCreateOrUpdateUserBilling()
    {
        if (UpdateUserBillingDto != null && UpdateUserBillingDto.Id != 0)
        {
            var result = await _userAdminClient.UpdateUserBillingAsync(UpdateUserBillingDto);
            Response.IsSuccessed = result.IsSuccessed;
            Response.Message = result.Message;
        }
        else if (CreateUserBillingDto != null)
        {
            var result = await _userAdminClient.CreateUserBillingAsync(CreateUserBillingDto);
            Response.IsSuccessed = result.IsSuccessed;
            Response.Message = result.Message;
        }

        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostDeleteUserBilling()
    {
        var result = await _userAdminClient.DeleteUserBillingAsync(billingId);
        Response.IsSuccessed = result.IsSuccessed;
        Response.Message = result.Message;
        return new JsonResult(Response);
    }

    #endregion

    #region User Shippings

    public async Task<IActionResult> OnPostGetUserShippings()
    {
        var result = await _userAdminClient.GetUserShippingsAsync(userId);
        if (result.IsSuccessed)
        {
            Response.Object = result.Object;
            Response.IsSuccessed = true;
        }
        else
        {
            Response.IsSuccessed = false;
            Response.Message = result.Message;
        }

        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostGetUserShipping()
    {
        var result = await _userAdminClient.GetUserShippingAsync(shippingId);
        if (result.IsSuccessed)
        {
            Response.Object = result.Object;
            Response.IsSuccessed = true;
        }
        else
        {
            Response.IsSuccessed = false;
            Response.Message = result.Message;
        }

        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostCreateOrUpdateUserShipping()
    {
        if (UpdateUserShippingDto != null && UpdateUserShippingDto.Id != 0)
        {
            var result = await _userAdminClient.UpdateUserShippingAsync(UpdateUserShippingDto);
            Response.IsSuccessed = result.IsSuccessed;
            Response.Message = result.Message;
        }
        else if (CreateUserShippingDto != null)
        {
            var result = await _userAdminClient.CreateUserShippingAsync(CreateUserShippingDto);
            Response.IsSuccessed = result.IsSuccessed;
            Response.Message = result.Message;
        }

        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostDeleteUserShipping()
    {
        var result = await _userAdminClient.DeleteUserShippingAsync(shippingId);
        Response.IsSuccessed = result.IsSuccessed;
        Response.Message = result.Message;
        return new JsonResult(Response);
    }

    #endregion
}