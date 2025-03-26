using Microsoft.EntityFrameworkCore;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Domain.Models;
using Parstech.Shop.Context.Persistence.Context;

namespace Parstech.Shop.Context.Persistence.Repositories;

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