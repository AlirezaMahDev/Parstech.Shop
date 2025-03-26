using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Contracts.Persistance;

public interface IProductPropertyRepository:IGenericRepository<ProductProperty>
{
    Task<ProductProperty?> GetpropertyByProduct(int productId);
    Task<List<ProductProperty>> GetPropertiesByProduct(int productId);
    Task<bool> ExistPropertyForProduct(int productId, int propertyId);
}