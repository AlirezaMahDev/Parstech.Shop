using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.Web.Services.GrpcClients;

namespace Parstech.Shop.Web.Pages.Admin.Feutures.Categuries;

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
    public ParameterDto Parameter { get; set; } = new ParameterDto();

    //categuries
    [BindProperty]
    public PagingDto List { get; set; }

    //categury
    [BindProperty]
    public PropertyCateguryDto PropertyCateguryDto { get; set; }

    [BindProperty]
    public int PropertycateguryId { get; set; }

    //result
    [BindProperty]
    public ResponseDto Response { get; set; } = new ResponseDto();

    #endregion

    #region Methods

    public async Task<IActionResult> OnGet()
    {
        // This page will be updated to use the PropertyAdminGrpcClient
        // For now, we'll redirect to the main Features page
        return RedirectToPage("/Admin/Feutures/Index");
    }

    public async Task<IActionResult> OnPostData()
    {
        // This functionality will be handled by the PropertyAdminGrpcClient
        // For now, we'll redirect to the main Features page
        return RedirectToPage("/Admin/Feutures/Index");
    }

    public async Task<IActionResult> OnPostSearch()
    {
        // This functionality will be handled by the PropertyAdminGrpcClient
        // For now, we'll redirect to the main Features page
        return RedirectToPage("/Admin/Feutures/Index");
    }

    public async Task<IActionResult> OnPostPaging()
    {
        // This functionality will be handled by the PropertyAdminGrpcClient
        // For now, we'll redirect to the main Features page
        return RedirectToPage("/Admin/Feutures/Index");
    }

    public async Task<IActionResult> OnPostCategury()
    {
        // This functionality will be handled by the PropertyAdminGrpcClient
        // For now, we'll redirect to the main Features page
        return RedirectToPage("/Admin/Feutures/Index");
    }

    public async Task<IActionResult> OnPostEditOrCreate()
    {
        // This functionality will be handled by the PropertyAdminGrpcClient
        // For now, we'll redirect to the main Features page
        return RedirectToPage("/Admin/Feutures/Index");
    }

    #endregion
}