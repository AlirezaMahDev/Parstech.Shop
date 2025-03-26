using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Contracts.Persistance;

public interface IUserCateguryRepository:IGenericRepository<UserCategury>
{
    Task<bool> ExistUserInCategury(int userId);
    Task<UserCategury> GetUserCateguryByUserId(int userId);
}