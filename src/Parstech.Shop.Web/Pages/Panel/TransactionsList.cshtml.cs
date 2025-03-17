using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.Web.Services.GrpcClients;

namespace Parstech.Shop.Web.Pages.Panel;

[Authorize]
public class TransactionsListModel : PageModel
{
    #region Constructor

    private readonly UserGrpcClient _userClient;
    private readonly WalletGrpcClient _walletClient;
    private readonly UserProfileGrpcClient _userProfileClient;

    public TransactionsListModel(
        UserGrpcClient userClient,
        WalletGrpcClient walletClient,
        UserProfileGrpcClient userProfileClient)
    {
        _userClient = userClient;
        _walletClient = walletClient;
        _userProfileClient = userProfileClient;
    }

    #endregion

    #region Properties

    [BindProperty]
    public ResponseDto Response { get; set; } = new ResponseDto();

    [BindProperty]
    public WalletDto Wallet { get; set; }

    [BindProperty]
    public WalletTransactionParameterDto Parameter { get; set; } = new WalletTransactionParameterDto();

    [BindProperty]
    public PagingDto List { get; set; }

    [BindProperty]
    public string Type { get; set; }

    [BindProperty]
    public int transactionId { get; set; }

    [BindProperty]
    public WalletTransactionDto transactionDto { get; set; }

    #endregion

    #region Get

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostData()
    {
        Parameter.TakePage = 30;
        var currentUser = await _userClient.GetUserByUsernameAsync(User.Identity.Name);
        var wallet = await _walletClient.GetWalletByUserIdAsync(currentUser.Id);

        Wallet = new WalletDto
        {
            WalletId = wallet.WalletId,
            UserId = wallet.UserId,
            Credit = wallet.Credit,
            UsedCredit = wallet.UsedCredit,
            RemainingCredit = wallet.RemainingCredit,
            LastUpdated = DateTime.TryParse(wallet.LastUpdated, out DateTime date) ? date : null
        };

        Parameter.WalletId = Wallet.WalletId;
        Parameter.Type = Type;

        var transactions = await _userProfileClient.GetUserTransactionsAsync(
            Wallet.WalletId,
            Parameter.CurrentPage,
            Parameter.TakePage,
            Parameter.Type);

        List = new PagingDto
        {
            CurrentPage = transactions.CurrentPage,
            PageCount = transactions.PageCount,
            RowCount = transactions.TotalCount,
            List = transactions.Transactions.Select(t => new WalletTransactionDto
                {
                    TransactionId = t.TransactionId,
                    TransactionDate = DateTime.TryParse(t.TransactionDate, out DateTime tDate) ? tDate : null,
                    Amount = t.Amount,
                    TypeName = t.TypeName,
                    Description = t.Description,
                    IsCredit = t.IsCredit
                })
                .ToList()
        };

        Response.Object = List;
        Response.IsSuccessed = true;

        return new JsonResult(Response);
    }

    #endregion

    #region TransactionDetail

    public async Task<IActionResult> OnPostTransactionDetail()
    {
        var transaction = await _userProfileClient.GetTransactionDetailsAsync(transactionId);

        transactionDto = new WalletTransactionDto
        {
            TransactionId = transaction.TransactionId,
            WalletId = transaction.WalletId,
            TypeName = transaction.TypeName,
            Amount = transaction.Amount,
            Description = transaction.Description,
            TrackingCode = transaction.TrackingCode,
            TransactionDate = DateTime.TryParse(transaction.TransactionDate, out DateTime date) ? date : null,
            Months = transaction.Months,
            MonthlyPayment = transaction.MonthlyPayment,
            IsActive = transaction.IsActive,
            IsCredit = transaction.IsCredit
        };

        Response.Object = transactionDto;
        return new JsonResult(Response);
    }

    #endregion
}