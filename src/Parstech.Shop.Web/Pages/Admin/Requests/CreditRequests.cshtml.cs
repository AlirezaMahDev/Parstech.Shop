using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Web.Services;

namespace Parstech.Shop.Web.Pages.Admin.Requests;

public class CreditRequestsModel : PageModel
{
    #region Constructor

    private readonly FormCreditGrpcClient _formCreditClient;

    public CreditRequestsModel(FormCreditGrpcClient formCreditClient)
    {
        _formCreditClient = formCreditClient;
    }

    #endregion

    #region Properties

    // Paging parameter
    [BindProperty]
    public ParameterDto Parameter { get; set; } = new();

    // Credit requests
    [BindProperty]
    public List<FormCreditDto> List { get; set; }

    // Response for operations
    [BindProperty]
    public ResponseDto Response { get; set; } = new();

    #endregion

    #region Get

    public async Task<IActionResult> OnGet()
    {
        return Page();
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostData(int skip, string filter = "", string fromDate = "", string toDate = "")
    {
        try
        {
            Parameter.TakePage = 30; // Default page size

            // Use the new paged method
            var result = await _formCreditClient.GetPagedFormCreditsAsync(
                skip,
                Parameter.TakePage,
                filter,
                fromDate,
                toDate);

            List = result.Items;
            Response.IsSuccessed = true;
            Response.Object = List;
            Response.Object2 = new { result.TotalCount, result.PageCount };

            return new JsonResult(Response);
        }
        catch (Exception ex)
        {
            Response.IsSuccessed = false;
            Response.Message = $"Error loading credit requests: {ex.Message}";
            return new JsonResult(Response);
        }
    }

    public async Task<IActionResult> OnPostChangeStatus(int id, string type)
    {
        try
        {
            // Use gRPC client to change status
            var result = await _formCreditClient.ChangeFormCreditStatusAsync(id, type);
            return new JsonResult(result);
        }
        catch (Exception ex)
        {
            Response.IsSuccessed = false;
            Response.Message = $"Error changing status: {ex.Message}";
            return new JsonResult(Response);
        }
    }

    #endregion
}