using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Web.Services;

namespace Parstech.Shop.Web.Pages.Panel;

[Authorize]
public class ShippingsModel : PageModel
{
    #region Constructor

    private readonly UserGrpcClient _userClient;
    private readonly UserProfileGrpcClient _userProfileClient;

    public ShippingsModel(
        UserGrpcClient userClient,
        UserProfileGrpcClient userProfileClient)
    {
        _userClient = userClient;
        _userProfileClient = userProfileClient;
    }

    #endregion

    #region Properties

    [BindProperty]
    public ParameterDto Parameter { get; set; } = new();

    [BindProperty]
    public ResponseDto Response { get; set; } = new();

    [BindProperty]
    public int UserId { get; set; }

    [BindProperty]
    public List<UserShippingDto> UserShippingsList { get; set; }

    [BindProperty]
    public UserShippingDto UserShippingDto { get; set; }

    [BindProperty]
    public int UserShippingId { get; set; }

    #endregion

    #region Get

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostData()
    {
        var currentUser = await _userClient.GetUserByUsernameAsync(User.Identity.Name);
        var shippings = await _userProfileClient.GetUserShippingAddressesAsync(currentUser.Id);

        UserShippingsList = new();
        foreach (var shipping in shippings.ShippingAddresses)
        {
            UserShippingsList.Add(new()
            {
                Id = shipping.Id,
                UserId = shipping.UserId,
                Address = shipping.Address,
                PostalCode = shipping.PostalCode,
                Mobile = shipping.Mobile,
                City = shipping.City,
                Province = shipping.Province,
                IsDefault = shipping.IsDefault
            });
        }

        Response.Object = UserShippingsList;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    #endregion

    #region Create Update

    public async Task<IActionResult> OnPostGetItem()
    {
        var shipping = await _userProfileClient.GetUserShippingByIdAsync(UserShippingId);

        UserShippingDto = new()
        {
            Id = shipping.Id,
            UserId = shipping.UserId,
            Address = shipping.Address,
            PostalCode = shipping.PostalCode,
            Mobile = shipping.Mobile,
            City = shipping.City,
            Province = shipping.Province,
            IsDefault = shipping.IsDefault
        };

        Response.Object = UserShippingDto;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostUpdateAndCreate()
    {
        if (UserShippingDto.Id == 0)
        {
            var currentUser = await _userClient.GetUserByUsernameAsync(User.Identity.Name);
            UserShippingDto.UserId = currentUser.Id;

            var shipping = await _userProfileClient.CreateUserShippingAsync(
                UserShippingDto.UserId,
                UserShippingDto.Address,
                UserShippingDto.PostalCode,
                UserShippingDto.Mobile,
                UserShippingDto.City,
                UserShippingDto.Province,
                UserShippingDto.IsDefault);

            UserShippingDto.Id = shipping.Id;
            Response.Object = UserShippingDto;
            Response.IsSuccessed = true;
            Response.Message = "آدرس با موفقیت ثبت شد";
            return new JsonResult(Response);
        }
        else
        {
            var shipping = await _userProfileClient.UpdateUserShippingAsync(
                UserShippingDto.Id,
                UserShippingDto.UserId,
                UserShippingDto.Address,
                UserShippingDto.PostalCode,
                UserShippingDto.Mobile,
                UserShippingDto.City,
                UserShippingDto.Province,
                UserShippingDto.IsDefault);

            Response.Object = UserShippingDto;
            Response.IsSuccessed = true;
            Response.Message = "آدرس با موفقیت ویرایش شد";
            return new JsonResult(Response);
        }
    }

    public async Task<IActionResult> OnPostDeleteItem()
    {
        var result = await _userProfileClient.DeleteUserShippingAsync(UserShippingId);

        Response.IsSuccessed = result.Success;
        Response.Message = result.Message;
        return new JsonResult(Response);
    }

    #endregion
}