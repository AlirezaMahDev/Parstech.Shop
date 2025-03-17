using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Contracts.Persistance;

public interface IWalletTransactionRepository : IGenericRepository<WalletTransaction>
{
    Task<bool> WalletIsWerifyForNewFecilities(int WalletId, int TransactionTypeId, string TypeName);
    FecilitiesDto GenerateNewFesilities(FecilitiesDto request);
    Task<FecilitiesDto> CreateNewFesilities(FecilitiesDto request);
    string GenerateWordOfFecilities(FecilitiesDto item);
    Task<WalletTransaction> GetLastOfOrder(string orderCode);
    Task<WalletTransaction> GetActiveAghsat(int walletId, string type);
    Task<WalletTransaction> GetParentFecilities(int parentId);
    Task<List<WalletTransaction>> GetAghsatByParentId(int parentId, int typeId);
    Task<WalletTransaction> GetTransactionByTrackingCode(string trackingCode);
    Task<bool> ExistTransactionForUser(int UserId, string TracsactionCode);
    Task<List<WalletTransaction>> GetTransactionsByParentId(int parentId);
    Task<WalletTransaction> GetByIdAsync(int transactionId);
    Task<WalletTransaction> GetByTokenAsync(string token);
    Task<List<WalletTransaction>> GetTransactionHistoryAsync(
        int walletId,
        int userId,
        int page,
        int pageSize,
        string fromDate,
        string toDate,
        int transactionTypeId);
    Task<int> GetTransactionHistoryCountAsync(
        int walletId,
        int userId,
        string fromDate,
        string toDate,
        int transactionTypeId);
}