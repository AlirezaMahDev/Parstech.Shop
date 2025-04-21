using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.Response;
using Shop.Application.DTOs.Wallet;
using Shop.Application.DTOs.WalletTransaction;
using Shop.Application.Features.User.Requests.Queries;
using Shop.Application.Features.Wallet.Requests.Queries;
using Shop.Application.Features.WalletTransaction.Requests.Queries;

namespace Shop.Web.Pages.Panel
{
   

    [Authorize]
    public class CreditTransactionsModel : PageModel
    {
        #region Constractor

        private readonly IMediator _mediator;
        public CreditTransactionsModel(IMediator mediator)
        {
            _mediator = mediator;
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
            Parameter.Type = "ProductCredit";
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
}
