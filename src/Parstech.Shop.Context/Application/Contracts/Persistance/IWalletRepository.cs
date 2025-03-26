using Parstech.Shop.Context.Application.DTOs.WalletTransaction;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Contracts.Persistance;

public interface IWalletRepository:IGenericRepository<Wallet>
{

    Task WalletCalculateTransaction(WalletTransactionDto walletTransactionDto);
    Task<long> GetRemainingOfWallet(int userId, string type);
    Task<Wallet> GetWalletByUserId(int userId);
    int GetCountOfWallets();
}