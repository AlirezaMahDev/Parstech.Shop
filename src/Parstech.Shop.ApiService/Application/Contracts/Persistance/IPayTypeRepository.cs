using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Contracts.Persistance;

public interface IPayTypeRepository : IGenericRepository<PayType>
{
    Task<List<PayType>> GetActiveList();
}