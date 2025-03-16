using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Parstech.Shop.Web.Services.GrpcClients;
using Shop.Application.DTOs.User;
using Shop.Application.DTOs.WalletTransaction;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Shop.Web.Pages.Admin.Reports.Finanical
{
    [Authorize(Roles = "SupperUser,Finanicial")]
    public class TransactionsModel : PageModel
    {
        #region Constructor
        private readonly IReportsAdminGrpcClient _reportsClient;
        
        public TransactionsModel(IReportsAdminGrpcClient reportsClient)
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
            result = await _reportsClient.GetTransactionsReportAsync(parameters);
            return new JsonResult(result);
        }
        
        public async Task<IActionResult> OnPostSearch()
        {
            parameters.TakePage = 100;
            result = await _reportsClient.GetTransactionsReportAsync(parameters);
            return new JsonResult(result);
        }

        public async Task<IActionResult> OnPostExcel(string userFilter, string walletType, int transactionType, string fromDate, string toDate)
        {
            var (fileData, fileName) = await _reportsClient.GenerateTransactionsExcelAsync(
                userFilter, walletType, transactionType, fromDate, toDate);
            
            return File(
                fileData,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName);
        }
    }
}
