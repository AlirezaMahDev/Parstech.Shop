using Shop.Application.DTOs.Categury;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.Property;
using Shop.Application.DTOs.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parstech.Shop.Web.Services.GrpcClients
{
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
} 