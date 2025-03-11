using ClosedXML.Excel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.DTOs.User;
using Shop.Application.DTOs.WalletTransaction;
using Shop.Application.Features.User.Requests.Queries;
using Shop.Application.Features.WalletTransaction.Requests.Queries;

namespace Shop.Web.Pages.Admin.Reports.Finanical
{
    [Authorize(Roles = "SupperUser,Finanicial")]
    public class TransactionsModel : PageModel
    {

        #region Constractor
        private readonly IMediator _mediator;
        public TransactionsModel(IMediator mediator)
        {
            _mediator = mediator;
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
            Users = await _mediator.Send(new GetUsersForSelectListQueryReq());
            return Page();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostGetData()
        {
            parameters.CurrentPage = 1;
            parameters.TakePage = 100;
            result = await _mediator.Send(new ReportOfWalletTransactionsQueryReq(parameters));
            return new JsonResult(result);
        }
        public async Task<IActionResult> OnPostSearch()
        {
            parameters.TakePage = 100;
            result = await _mediator.Send(new ReportOfWalletTransactionsQueryReq(parameters));
            return new JsonResult(result);
        }

        public async Task<IActionResult> OnPostExcel(string userFilter, string walletType, int transactionType, string fromDate, string toDate)
        {
            parameters.TakePage = -1;
            parameters.UserFilter = userFilter;
            parameters.WalletType = walletType;
            parameters.TransactionType = transactionType;
            parameters.FromDate = fromDate;
            parameters.ToDate = toDate;
            var result = await _mediator.Send(new ReportOfWalletTransactionsQueryReq(parameters));
            using (var workbook = new XLWorkbook())
            {

                var worksheet = workbook.Worksheets.Add("Transactions");
                worksheet.SetRightToLeft();
                worksheet.ColumnWidth = 30;
                worksheet.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);


                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "نام کاربری";
                worksheet.Cell(currentRow, 2).Value = " نام مشتری";
                worksheet.Cell(currentRow, 3).Value = "شناسه تراکنش";
                worksheet.Cell(currentRow, 4).Value = "کد پیگیری";
                worksheet.Cell(currentRow, 5).Value = "موجودی کیف پول";
                worksheet.Cell(currentRow, 6).Value = "موجودی تسهیلات";
                worksheet.Cell(currentRow, 7).Value = "موجودی اعتبار سازمانی";
                worksheet.Cell(currentRow, 8).Value = "مبلغ تراکنش";
                worksheet.Cell(currentRow, 9).Value = "نوع حساب";
                worksheet.Cell(currentRow, 10).Value = "نوع تراکنش";
                worksheet.Cell(currentRow, 11).Value = "توضیحات";
                worksheet.Cell(currentRow, 12).Value = "تاریخ";
                worksheet.Cell(currentRow, 13).Value = "شناسه تسهیلات";
                worksheet.Cell(currentRow, 14).Value = "وضعیت شروع";
                worksheet.Cell(currentRow, 15).Value = "فعال/فیرغعال";


                foreach (var item in result.walletTransactions)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = item.UserName;
                    worksheet.Cell(currentRow, 2).Value = $"{item.FirstName} {item.LastName}";
                    worksheet.Cell(currentRow, 3).Value = item.Id;
                    worksheet.Cell(currentRow, 4).Value = item.TrackingCode;
                    worksheet.Cell(currentRow, 5).Value = item.Amount;
                    worksheet.Cell(currentRow, 6).Value = item.Fecilities;
                    worksheet.Cell(currentRow, 7).Value = item.OrgCredit;
                    worksheet.Cell(currentRow, 8).Value = item.Price;
                    worksheet.Cell(currentRow, 9).Value = item.Type;
                    worksheet.Cell(currentRow, 10).Value = item.TypeTitle;
                    worksheet.Cell(currentRow, 11).Value = item.Description;
                    worksheet.Cell(currentRow, 12).Value = item.CreateDateShamsi;
                    worksheet.Cell(currentRow, 13).Value = item.ParentFecilitiesId;
                    worksheet.Cell(currentRow, 14).Value = item.Start;
                    worksheet.Cell(currentRow, 15).Value = item.Active.Value;

                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Reports.xlsx");
                }
            }


        }
    }
}
