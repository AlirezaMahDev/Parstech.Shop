using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Contracts.Persistance;

public interface IUserProductRepository : IGenericRepository<UserProduct>
{
    Task<List<UserProduct?>> GetUserProductsByUsername(string userName, string type);
    Task<bool> ExistFourUserProductByUserName(string userName, string type);
}