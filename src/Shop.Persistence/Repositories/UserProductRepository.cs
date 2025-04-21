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
    public class UserProductRepository : GenericRepository<UserProduct>, IUserProductRepository
    {
        private readonly DatabaseContext _context;
        private readonly IUserRepository _userRep;
        public UserProductRepository(DatabaseContext context, IUserRepository userRepository) : base(context)
        {
            _context = context;
            _userRep = userRepository;
        }



        public async Task<bool> ExistFourUserProductByUserName(string userName, string type)
        {
            var userProducts =await GetUserProductsByUsername(userName, type);
            if (userProducts.Count < 4)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<List<UserProduct?>> GetUserProductsByUsername(string userName, string type)
        {
            var user = await _userRep.GetUserByUserName(userName);
            var userProducts = await _context.UserProducts.Where(u => u.UserId == user.Id && u.Type == type).ToListAsync();

            return userProducts;
        }
    }
}
