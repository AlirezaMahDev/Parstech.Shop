using Microsoft.EntityFrameworkCore;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Domain.Models;
using Parstech.Shop.ApiService.Persistence.Context;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

public class UserCateguryRepository : GenericRepository<UserCategury>, IUserCateguryRepository
{
    private readonly DatabaseContext _context;

    public UserCateguryRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> ExistUserInCategury(int userId)
    {
        if (await _context.UserCateguries.AnyAsync(u => u.CateguryId == 1 && u.UserId == userId))
        {
            return true;
        }

        return false;
    }

    public async Task<UserCategury> GetUserCateguryByUserId(int userId)
    {
        return await _context.UserCateguries.FirstOrDefaultAsync(u => u.CateguryId == 1 && u.UserId == userId);
    }
}