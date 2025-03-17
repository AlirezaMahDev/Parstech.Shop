using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Contracts.Persistance;

public interface IProductPropertyRepository : IGenericRepository<ProductProperty>
{
    Task<ProductProperty?> GetpropertyByProduct(int productId);
    Task<List<ProductProperty>> GetPropertiesByProduct(int productId);
    Task<bool> ExistPropertyForProduct(int productId, int propertyId);
}