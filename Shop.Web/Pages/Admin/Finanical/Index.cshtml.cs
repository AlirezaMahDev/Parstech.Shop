using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.Response;
using Shop.Application.DTOs.User;
using Shop.Application.DTOs.WalletTransaction;
using Shop.Application.Features.User.Requests.Queries;
using Shop.Application.Features.Wallet.Requests.Queries;
using Shop.Application.Features.WalletTransaction.Requests.Commands;
using Shop.Application.Features.WalletTransaction.Requests.Queries;
using Shop.Application.Validators.WalletTransaction;

namespace Shop.Web.Pages.Admin.Finanical
{
    [Authorize(Roles = "SupperUser,Finanicial,Store,Sale")]
    public class IndexModel : PageModel
    {
        #region Constractor

        private readonly IMediator _mediator;

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region Properties

        //paging parameter
        [BindProperty]
        public ParameterDto Parameter { get; set; } = new ParameterDto();

        [BindProperty]
        public WalletTransactionParameterDto WTParameter { get; set; } = new WalletTransactionParameterDto();

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
        public ResponseDto Response { get; set; } = new ResponseDto();

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
            UserFilterDtos = await _mediator.Send(new UserFilterDataQueryReq());
            return Page();
        }

        public async Task<IActionResult> OnPostData()
        {


            Parameter.TakePage = 30;
            List = await _mediator.Send(new WalletPagingQueryReq(Parameter));
            Response.Object = List;
            Response.IsSuccessed = true;

            return new JsonResult(Response);
        }

        #endregion

        #region Search Paging

        public async Task<IActionResult> OnPostSearch()
        {

            Parameter.TakePage = 30;
            List = await _mediator.Send(new WalletPagingQueryReq(Parameter));
            Response.Object = List;
            Response.IsSuccessed = true;
            return new JsonResult(Response);
        }

        public async Task<IActionResult> OnPostPaging()
        {
            Parameter.TakePage = 30;
            List = await _mediator.Send(new WalletPagingQueryReq(Parameter));
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
            TransactionList = await _mediator.Send(new WalletTransactionsPagingQueryReq(WTParameter));
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
            var result = await _mediator.Send(new CreateWalletTransactionCommandReq(Transaction, true));
            if (result.isSuccessed)
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
            //var result =await _mediator.Send(new CreateAghsatQueryReq(Fecilities));
            //if (result)
            //{
            //    Response.IsSuccessed = true;
            //}
            //else
            //{
            //    Response.IsSuccessed = false;
            //    Response.Message = "تا زمانی که تسهیلات کاربر تسویه نگردد امکان ثبت تسهیلات جدید وجود ندارد.";
            //}
            return new JsonResult(Response);
        }

        #endregion

        #region Block Or Unblock
        public async Task<IActionResult> OnPostBlockOrUnblock()
        {
            await _mediator.Send(new WalletBlockOrUnblockQueryReq(block, walletId));
            return new JsonResult(Response);
        }
        #endregion

        #region TransactionDetail

        public async Task<IActionResult> OnPostTransactionDetail()
        {
            transactionDto = await _mediator.Send(new WalletTransactionDetailShowQueryReq(transactionId));
            Response.Object = transactionDto;
            return new JsonResult(Response);
        }

        #endregion

        #region Tasviye Ghest

        public async Task<IActionResult> OnPostTasviyeGhest(int transactionId)
        {
            Response = await _mediator.Send(new GhestPaymentQueryReq(transactionId));

            return new JsonResult(Response);
        }

        #endregion

        #region Registration Fecilities

        public async Task<IActionResult> OnPostRegistrationFecilities(string type, IFormFile file)
        {
            Response = await _mediator.Send(new FacilityRegistrationByExcelQueryReq(type, file));

            return new JsonResult(Response);
        }

        #endregion

        #region Payment Fecilities

        public async Task<IActionResult> OnPostPaymentFecilities(IFormFile file)
        {
            Response = await _mediator.Send(new FacilityPaymentByExcelQueryReq(file));

            return new JsonResult(Response);
        }

        #endregion
    }
}
