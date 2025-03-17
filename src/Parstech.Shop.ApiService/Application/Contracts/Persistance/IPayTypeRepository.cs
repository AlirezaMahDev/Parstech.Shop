using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Application.Contracts.Persistance;

public interface IPayTypeRepository : IGenericRepository<PayType>
{
    Task<List<PayType>> GetActiveList();
}