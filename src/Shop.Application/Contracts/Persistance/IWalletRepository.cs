using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.Wallet;
using Shop.Application.DTOs.WalletTransaction;
using Shop.Domain.Models;

namespace Shop.Application.Contracts.Persistance
{
    public interface IWalletRepository:IGenericRepository<Wallet>
    {

        Task WalletCalculateTransaction(WalletTransactionDto walletTransactionDto);
        Task<long> GetRemainingOfWallet(int userId, string type);
        Task<Wallet> GetWalletByUserId(int userId);
        int GetCountOfWallets();
    }
}
