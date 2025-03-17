using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Contracts.Persistance;

public interface IWalletRepository : IGenericRepository<Wallet>
{
    Task WalletCalculateTransaction(WalletTransactionDto walletTransactionDto);
    Task<long> GetRemainingOfWallet(int userId, string type);
    Task<Wallet> GetWalletByUserId(int userId);
    int GetCountOfWallets();
}