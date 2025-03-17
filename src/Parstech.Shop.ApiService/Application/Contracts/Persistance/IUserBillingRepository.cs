using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Application.Contracts.Persistance;

public interface IUserBillingRepository : IGenericRepository<UserBilling>
{
    Task<UserBilling?> GetUserBillingByUserId(int userId);
    Task<int> ExistBillingForPersonalId(string EconomicCode);
}