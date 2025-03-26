using Parstech.Shop.Context.Application.DTOs.WalletTransaction;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Contracts.Persistance;

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

}