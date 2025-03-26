using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Contracts.Persistance;

public interface IUserBillingRepository:IGenericRepository<UserBilling>
{
    Task<UserBilling?> GetUserBillingByUserId(int userId);
    Task<int> ExistBillingForPersonalId(string EconomicCode);
        
}