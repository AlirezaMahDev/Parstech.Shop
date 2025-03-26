using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Contracts.Persistance;

public interface ISecoundPayAfterDargahRepository:IGenericRepository<SecoundPayAfterDargah>
{
    Task<SecoundPayAfterDargah>GetByOrderId(int orderId);
}