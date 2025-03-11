using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Coupon;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.Wallet;
using Shop.Application.DTOs.WalletTransaction;
using Shop.Domain.Models;
using Shop.Persistence.Context;

namespace Shop.Persistence.Repositories
{
    public class WalletRepository : GenericRepository<Wallet>, IWalletRepository
    {
        private readonly DatabaseContext _context;
        private readonly IUserBillingRepository _userBillingRep;
        private readonly IMapper _mapper;

        public WalletRepository(DatabaseContext context, IUserBillingRepository userBillingRep, IMapper mapper) : base(context)
        {
            _context = context;
            _userBillingRep = userBillingRep;
            _mapper = mapper;
        }


        public async Task WalletCalculateTransaction(WalletTransactionDto walletTransactionDto)
        {
            var wallet = await GetAsync(walletTransactionDto.WalletId);
            switch (walletTransactionDto.Type)
            {
                case "Amount":
                    switch (walletTransactionDto.TypeId)
                    {
                        case 1:
                            wallet.Amount +=  walletTransactionDto.Price;
                            break;
                        case 2:
                            wallet.Amount -=  walletTransactionDto.Price;
                            break;
                    }
                    break;
                case "Coin":
                    switch (walletTransactionDto.TypeId)
                    {
                        case 1:
                            wallet.Coin +=  walletTransactionDto.Price;
                            break;
                        case 2:
                            wallet.Coin -= walletTransactionDto.Price;
                            break;
                    }
                    break;
                case "Fecilities":
                    switch (walletTransactionDto.TypeId)
                    {
                        case 1:
                            wallet.Fecilities += walletTransactionDto.Price;
                            break;
                        case 2:
                            wallet.Fecilities -=  walletTransactionDto.Price;
                            break;
                    }
                    break;
                case "OrgCredit":
                    switch (walletTransactionDto.TypeId)
                    {
                        case 1:
                            wallet.OrgCredit +=  walletTransactionDto.Price;
                            break;
                        case 2:
                            wallet.OrgCredit -=  walletTransactionDto.Price;
                            break;
                    }
                    break;
            }
            await UpdateAsync(wallet);
        }
        public int GetCountOfWallets()
        {
           return _context.Wallets.Count();
        }
        public async Task<long> GetRemainingOfWallet(int userId, string type)
        {
            var wallet = await _context.Wallets.SingleOrDefaultAsync(z => z.UserId == userId);
            long result = 0;
            switch (type)
            {
                case "Amount":
                    result = wallet.Amount;
                    break;
                case "Coin":
                    result = wallet.Coin;
                    break;
                case "Fecilities":
                    result = wallet.Fecilities;
                    break;
            }
            return result;
        }

        public async Task<Wallet> GetWalletByUserId(int userId)
        {
            return await _context.Wallets.SingleOrDefaultAsync(z => z.UserId == userId);
        }
    }
}
