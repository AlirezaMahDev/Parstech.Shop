using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Contracts.Persistance;

public interface IUserCateguryRepository : IGenericRepository<UserCategury>
{
    Task<bool> ExistUserInCategury(int userId);
    Task<UserCategury> GetUserCateguryByUserId(int userId);
}