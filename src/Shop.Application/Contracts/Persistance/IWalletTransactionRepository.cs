using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.WalletTransaction;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Contracts.Persistance
{
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

        Task<bool> ExistCreditForUser(int UserId);

    }
}
