using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Contracts.Persistance;

public interface IBrandRepository : IGenericRepository<Brand>
{
    int GetCountOfBrands();
}