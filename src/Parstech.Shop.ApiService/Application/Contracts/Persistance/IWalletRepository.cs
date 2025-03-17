using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Application.Contracts.Persistance;

public interface IWalletRepository : IGenericRepository<Wallet>
{
    Task WalletCalculateTransaction(WalletTransactionDto walletTransactionDto);
    Task<long> GetRemainingOfWallet(int userId, string type);
    Task<Wallet> GetWalletByUserId(int userId);
    int GetCountOfWallets();
}