using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Web.Services;

namespace Parstech.Shop.Web.Pages.Admin.Feutures;

[Authorize(Roles = "SupperUser,Sale")]
public class IndexModel : PageModel
{
    #region Constractor

    private readonly PropertyAdminGrpcClient _propertyClient;

    public IndexModel(PropertyAdminGrpcClient propertyClient)
    {
        _propertyClient = propertyClient;
    }

    #endregion

    #region Properties

    //paging parameter
    [BindProperty]
    public PropertyParameterDto Parameter { get; set; } = new();

    //categuries
    [BindProperty]
    public PagingDto List { get; set; }

    //categury
    [BindProperty]
    public PropertyDto PropertyDto { get; set; }

    [BindProperty]
    public int PropertyId { get; set; }

    //result
    [BindProperty]
    public ResponseDto Response { get; set; } = new();

    #endregion

    #region Methods

    public async Task OnGet()
    {
        Parameter.PageIndex = 1;
        Parameter.PageSize = 10;
        List = await _propertyClient.GetPropertiesAsync(Parameter);
    }

    public async Task<IActionResult> OnPostData()
    {
        List = await _propertyClient.GetPropertiesAsync(Parameter);
        return Page();
    }

    public async Task<IActionResult> OnPostSave()
    {
        if (PropertyDto.Id == 0)
        {
            Response = await _propertyClient.CreatePropertyAsync(PropertyDto);
        }
        else
        {
            Response = await _propertyClient.UpdatePropertyAsync(PropertyDto);
        }

        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostGetAllCateguries()
    {
        var categuries = await _propertyClient.GetAllCategoriesAsync();
        return new JsonResult(categuries);
    }

    public async Task<IActionResult> OnPostEditOrCreateCategury(int propertyId, int categoryId)
    {
        Response = await _propertyClient.CreateOrUpdatePropertyCategoryAsync(propertyId, categoryId);
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostGetById(int id)
    {
        PropertyDto = await _propertyClient.GetPropertyByIdAsync(id);
        return new JsonResult(PropertyDto);
    }

    public async Task<IActionResult> OnPostDelete(int id)
    {
        Response = await _propertyClient.DeletePropertyAsync(id);
        return new JsonResult(Response);
    }

    #endregion
}