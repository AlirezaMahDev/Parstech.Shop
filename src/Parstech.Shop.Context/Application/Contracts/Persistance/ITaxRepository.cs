using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Contracts.Persistance;

public interface ITaxRepository:IGenericRepository<Tax>
{
    Task<long> TaxCalculate(long Price);
}