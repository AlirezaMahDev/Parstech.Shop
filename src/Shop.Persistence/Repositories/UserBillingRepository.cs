using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Contracts.Persistance;
using Shop.Domain.Models;
using Shop.Persistence.Context;

namespace Shop.Persistence.Repositories
{
    public class UserBillingRepository:GenericRepository<UserBilling>,IUserBillingRepository
    {
        private readonly DatabaseContext _context;

        public UserBillingRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> ExistBillingForPersonalId(string EconomicCode)
        {
            if (await _context.UserBillings.AnyAsync(u => u.EconomicCode == EconomicCode))
            {
                var billing =await _context.UserBillings.FirstOrDefaultAsync(u => u.EconomicCode == EconomicCode);
                return billing.UserId;
            }
            else
            {
                return 0;
            }
        }

        public async Task<UserBilling?> GetUserBillingByUserId(int userId)
        {
            return await _context.UserBillings.SingleOrDefaultAsync(u => u.UserId == userId);
        }
    }
}
