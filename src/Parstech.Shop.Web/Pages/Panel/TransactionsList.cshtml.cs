using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Context.Application.DTOs.Paging;
using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.DTOs.Wallet;
using Parstech.Shop.Context.Application.DTOs.WalletTransaction;
using Parstech.Shop.Context.Application.Features.User.Requests.Queries;
using Parstech.Shop.Context.Application.Features.Wallet.Requests.Queries;
using Parstech.Shop.Context.Application.Features.WalletTransaction.Requests.Queries;

namespace Parstech.Shop.Web.Pages.Panel;

[Authorize]
public class TransactionsListModel : PageModel
{
    #region Constractor

    private readonly IMediator _mediator;
    public TransactionsListModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    #region Properties

    [BindProperty]
    public ResponseDto Response { get; set; } = new();

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
    public async Task<IActionResult> OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostData()
    {

        Parameter.TakePage = 30;
        var CurrentUser = await _mediator.Send(new UserReadByUserNameQueryReq(User.Identity.Name));
        Wallet = await _mediator.Send(new GetWalletByUserIdQueryReq(CurrentUser.Id));
        Parameter.WalletId = Wallet.WalletId;
        Parameter.Type = Type;
        List = await _mediator.Send(new WalletTransactionsPagingQueryReq(Parameter));
        Response.Object = List;
        Response.IsSuccessed = true;

        return new JsonResult(Response);
    }
    #endregion
    #region Search Paging

    #region TransactionDetail

    public async Task<IActionResult> OnPostTransactionDetail()
    {
        transactionDto = await _mediator.Send(new WalletTransactionDetailShowQueryReq(transactionId));
        Response.Object = transactionDto;
        return new JsonResult(Response);
    }

    #endregion


    #endregion
}