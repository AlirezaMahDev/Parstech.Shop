using Microsoft.EntityFrameworkCore;
using Shop.Application.Contracts.Persistance;
using Shop.Domain.Models;
using Shop.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Persistence.Repositories
{
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
}
