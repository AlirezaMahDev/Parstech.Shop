using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Application.Contracts.Persistance;

public interface ISecoundPayAfterDargahRepository : IGenericRepository<SecoundPayAfterDargah>
{
    Task<SecoundPayAfterDargah> GetByOrderId(int orderId);
}