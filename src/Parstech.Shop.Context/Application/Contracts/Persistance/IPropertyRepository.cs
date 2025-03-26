using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Contracts.Persistance;

public interface IPropertyRepository:IGenericRepository<Property>
{
    Task<List<Property>>GetPropertyBySearch(int categuryId,int propertyCateguryId, string filter);

    Task<List<Property>> GetPropertiesOfCategory (int categoryId);
    Task<bool> ExistProperty (string propertyName);
    Task<Property?> GetByName(string propertyName);
    Task<bool> ExistPropertyForCateguryId(int categuryId);
}