using Microsoft.EntityFrameworkCore;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Domain.Models;
using Parstech.Shop.Context.Persistence.Context;

namespace Parstech.Shop.Context.Persistence.Repositories;

public class UserCateguryRepository: GenericRepository<UserCategury>,IUserCateguryRepository
{
    private readonly DatabaseContext _context;
    public UserCateguryRepository(DatabaseContext context):base(context)
    {
        _context = context;
            
    }

    public async Task<bool> ExistUserInCategury(int userId)
    {
        if (await _context.UserCateguries.AnyAsync(u => u.CateguryId == 1 && u.UserId == userId)){
            return true;
        }
        return false;
    }

    public async Task<UserCategury> GetUserCateguryByUserId(int userId)
    {
            
        return await _context.UserCateguries.FirstOrDefaultAsync(u => u.CateguryId == 1 && u.UserId == userId);
    }
}