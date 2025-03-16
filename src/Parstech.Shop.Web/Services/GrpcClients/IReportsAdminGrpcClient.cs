using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Application.DTOs.User;
using Shop.Application.DTOs.WalletTransaction;

namespace Parstech.Shop.Web.Services.GrpcClients
{
    public interface IReportsAdminGrpcClient
    {
        Task<List<UserForSelectListDto>> GetUsersForSelectListAsync();
        Task<WalletTransactionPagingDto> GetTransactionsReportAsync(TransactionParameterDto parameter);
        Task<WalletTransactionPagingDto> GetActiveCreditReportAsync(TransactionParameterDto parameter);
        Task<WalletTransactionPagingDto> GetActiveInstallmentsAsync(int userId);
        Task<(byte[] FileData, string FileName)> GenerateTransactionsExcelAsync(string userFilter, string walletType, int transactionType, string fromDate, string toDate);
        Task<(byte[] FileData, string FileName)> GenerateActiveCreditExcelAsync(string userFilter, string walletType, int transactionType, string fromDate, string toDate);
    }
} 