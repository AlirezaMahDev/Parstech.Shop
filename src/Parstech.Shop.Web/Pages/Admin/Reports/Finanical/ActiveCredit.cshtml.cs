using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.Web.Services.GrpcClients;

namespace Parstech.Shop.Web.Pages.Admin.Reports.Finanical;

public class ActiveCreditModel : PageModel
{
    #region Constructor

    private readonly IReportsAdminGrpcClient _reportsClient;

    public ActiveCreditModel(IReportsAdminGrpcClient reportsClient)
    {
        _reportsClient = reportsClient;
    }

    #endregion

    #region Properties

    [BindProperty]
    public TransactionParameterDto parameters { get; set; }

    public WalletTransactionPagingDto result { get; set; }
    public List<UserForSelectListDto> Users { get; set; }

    #endregion

    public async Task<IActionResult> OnGet()
    {
        Users = await _reportsClient.GetUsersForSelectListAsync();
        return Page();
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostGetData()
    {
        parameters.CurrentPage = 1;
        parameters.TakePage = 100;
        result = await _reportsClient.GetActiveCreditReportAsync(parameters);
        return new JsonResult(result);
    }

    public async Task<IActionResult> OnPostSearch()
    {
        parameters.TakePage = 100;
        result = await _reportsClient.GetActiveCreditReportAsync(parameters);
        return new JsonResult(result);
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostGetAghsat(int id)
    {
        result = await _reportsClient.GetActiveInstallmentsAsync(id);
        return new JsonResult(result);
    }

    public async Task<IActionResult> OnPostExcel(string userFilter,
        string walletType,
        int transactionType,
        string fromDate,
        string toDate)
    {
        (byte[]? fileData, string? fileName) = await _reportsClient.GenerateActiveCreditExcelAsync(
            userFilter,
            walletType,
            transactionType,
            fromDate,
            toDate);

        return File(
            fileData,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileName);
    }
}