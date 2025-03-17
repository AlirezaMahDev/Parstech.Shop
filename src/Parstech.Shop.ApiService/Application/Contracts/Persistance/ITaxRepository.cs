using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Application.Contracts.Persistance;

public interface ITaxRepository : IGenericRepository<Tax>
{
    Task<long> TaxCalculate(long Price);
}