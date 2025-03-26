using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Contracts.Persistance;

public interface IPayTypeRepository:IGenericRepository<PayType>
{
    Task<List<PayType>> GetActiveList();
}