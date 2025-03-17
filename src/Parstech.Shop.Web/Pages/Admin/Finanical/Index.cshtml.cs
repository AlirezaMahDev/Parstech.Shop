using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Web.Services;

namespace Parstech.Shop.Web.Pages.Admin.Finanical;

[Authorize(Roles = "SupperUser,Finanicial,Store,Sale")]
public class IndexModel : PageModel
{
    #region Constructor

    private readonly IFinancialAdminGrpcClient _financialClient;

    public IndexModel(IFinancialAdminGrpcClient financialClient)
    {
        _financialClient = financialClient;
    }

    #endregion

    #region Properties

    //paging parameter
    [BindProperty]
    public ParameterDto Parameter { get; set; } = new();

    [BindProperty]
    public WalletTransactionParameterDto WTParameter { get; set; } = new();

    //wallets
    [BindProperty]
    public PagingDto List { get; set; }

    [BindProperty]
    public PagingDto TransactionList { get; set; }

    [BindProperty]
    public int walletId { get; set; }

    [BindProperty]
    public string walletType { get; set; }

    [BindProperty]
    public FecilitiesDto Fecilities { get; set; }

    [BindProperty]
    public WalletTransactionDto Transaction { get; set; }

    //result
    [BindProperty]
    public ResponseDto Response { get; set; } = new();

    [BindProperty]
    public bool block { get; set; }

    [BindProperty]
    public int transactionId { get; set; }

    [BindProperty]
    public WalletTransactionDto transactionDto { get; set; }


    [BindProperty]
    public List<UserFilterDto> UserFilterDtos { get; set; }

    #endregion

    #region Get

    public async Task<IActionResult> OnGet()
    {
        UserFilterDtos = await _financialClient.GetUserFiltersAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostData()
    {
        Parameter.TakePage = 30;
        List = await _financialClient.GetWalletsPagingAsync(Parameter);
        Response.Object = List;
        Response.IsSuccessed = true;

        return new JsonResult(Response);
    }

    #endregion

    #region Search Paging

    public async Task<IActionResult> OnPostSearch()
    {
        Parameter.TakePage = 30;
        List = await _financialClient.GetWalletsPagingAsync(Parameter);
        Response.Object = List;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostPaging()
    {
        Parameter.TakePage = 30;
        List = await _financialClient.GetWalletsPagingAsync(Parameter);
        Response.Object = List;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    #endregion

    #region Transactions

    public async Task<IActionResult> OnPostDataTransactions()
    {
        WTParameter.TakePage = 30;
        WTParameter.WalletId = walletId;
        WTParameter.Type = walletType;
        TransactionList = await _financialClient.GetWalletTransactionsPagingAsync(WTParameter);
        Response.Object = TransactionList;
        Response.IsSuccessed = true;

        return new JsonResult(Response);
    }

    #endregion

    #region Fecilities

    public async Task<IActionResult> OnPostCreateTransaction()
    {
        #region Validator

        var validator = new TransactionValidator();
        var valid = validator.Validate(Transaction);
        if (!valid.IsValid)
        {
            Response.IsSuccessed = false;
            Response.Errors = valid.Errors;
            Response.Object = Transaction;
            return new JsonResult(Response);
        }

        #endregion

        var inputPrice = Transaction.InputPrice.Replace(",", "");
        Transaction.Price = int.Parse(inputPrice);
        Transaction.Start = false;
        Transaction.Description = "ثبت تسهیلات جدید";

        var result = await _financialClient.CreateWalletTransactionAsync(Transaction);

        if (result.IsSuccessed)
        {
            Response.IsSuccessed = true;
        }
        else
        {
            Response.IsSuccessed = false;
            Response.Message = "تا زمانی که تسهیلات کاربر تسویه نگردد امکان ثبت تسهیلات جدید وجود ندارد.";
        }

        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostCreateFecilities()
    {
        var result = await _financialClient.CreateFacilitiesAsync(Fecilities);

        if (result.IsSuccessed)
        {
            Response.IsSuccessed = true;
        }
        else
        {
            Response.IsSuccessed = false;
            Response.Message = result.Message ??
                               "تا زمانی که تسهیلات کاربر تسویه نگردد امکان ثبت تسهیلات جدید وجود ندارد.";
        }

        return new JsonResult(Response);
    }

    #endregion

    #region Block Or Unblock

    public async Task<IActionResult> OnPostBlockOrUnblock()
    {
        bool result = await _financialClient.BlockOrUnblockWalletAsync(block, walletId);
        Response.IsSuccessed = result;
        return new JsonResult(Response);
    }

    #endregion

    #region TransactionDetail

    public async Task<IActionResult> OnPostTransactionDetail()
    {
        transactionDto = await _financialClient.GetWalletTransactionDetailAsync(transactionId);
        Response.Object = transactionDto;
        return new JsonResult(Response);
    }

    #endregion

    #region Tasviye Ghest

    public async Task<IActionResult> OnPostTasviyeGhest(int transactionId)
    {
        Response = await _financialClient.PayInstallmentAsync(transactionId);
        return new JsonResult(Response);
    }

    #endregion

    #region Registration Fecilities

    public async Task<IActionResult> OnPostRegistrationFecilities(string type, IFormFile file)
    {
        Response = await _financialClient.RegisterFacilitiesByExcelAsync(type, file);
        return new JsonResult(Response);
    }

    #endregion

    #region Payment Fecilities

    public async Task<IActionResult> OnPostPaymentFecilities(IFormFile file)
    {
        Response = await _financialClient.ProcessFacilityPaymentsByExcelAsync(file);
        return new JsonResult(Response);
    }

    #endregion
}