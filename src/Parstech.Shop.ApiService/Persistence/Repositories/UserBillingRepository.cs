using Microsoft.EntityFrameworkCore;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Domain.Models;
using Parstech.Shop.ApiService.Persistence.Context;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

public class UserBillingRepository : GenericRepository<UserBilling>, IUserBillingRepository
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
            UserBilling? billing = await _context.UserBillings.FirstOrDefaultAsync(u => u.EconomicCode == EconomicCode);
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