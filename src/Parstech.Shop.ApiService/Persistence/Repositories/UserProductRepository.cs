using Microsoft.EntityFrameworkCore;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Persistence.Context;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

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
        List<UserProduct> userProducts = await GetUserProductsByUsername(userName, type);
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
        User? user = await _userRep.GetUserByUserName(userName);
        List<UserProduct> userProducts =
            await _context.UserProducts.Where(u => u.UserId == user.Id && u.Type == type).ToListAsync();

        return userProducts;
    }
}