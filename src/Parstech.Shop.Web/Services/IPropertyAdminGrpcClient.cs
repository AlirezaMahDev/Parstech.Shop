using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.Web.Services;

public interface IPropertyAdminGrpcClient
{
    Task<PropertyDto> GetPropertyByIdAsync(int propertyId);
    Task<PagingDto> GetPropertiesAsync(PropertyParameterDto parameter);
    Task<ResponseDto> CreatePropertyAsync(PropertyDto property);
    Task<ResponseDto> UpdatePropertyAsync(PropertyDto property);
    Task<ResponseDto> DeletePropertyAsync(int propertyId);
    Task<List<CateguryDto>> GetAllCategoriesAsync();
    Task<ResponseDto> CreateOrUpdatePropertyCategoryAsync(int propertyId, int categoryId);
}