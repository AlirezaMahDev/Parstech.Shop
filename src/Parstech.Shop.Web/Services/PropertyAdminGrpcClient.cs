using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.Web.Services;

public class PropertyAdminGrpcClient : GrpcClientBase, IPropertyAdminGrpcClient
{
    private readonly PropertyAdminService.PropertyAdminServiceClient _client;

    public PropertyAdminGrpcClient(IConfiguration configuration) : base(configuration)
    {
        _client = new PropertyAdminService.PropertyAdminServiceClient(Channel);
    }

    public async Task<PropertyDto> GetPropertyByIdAsync(int propertyId)
    {
        var request = new PropertyRequest { PropertyId = propertyId };
        var response = await _client.GetPropertyByIdAsync(request);

        return MapToPropertyDto(response);
    }

    public async Task<PagingDto> GetPropertiesAsync(PropertyParameterDto parameter)
    {
        var request = new PropertyParameterRequest
        {
            PageIndex = parameter.PageIndex,
            PageSize = parameter.PageSize,
            SearchKey = parameter.SearchKey ?? string.Empty
        };

        var response = await _client.GetPropertiesAsync(request);

        return MapToPagingDto(response);
    }

    public async Task<ResponseDto> CreatePropertyAsync(PropertyDto property)
    {
        var request = new PropertyRequest
        {
            Title = property.Title,
            SortOrder = property.SortOrder,
            IsColor = property.IsColor,
            IsActive = property.IsActive
        };

        var response = await _client.CreatePropertyAsync(request);

        return new() { Status = response.Status, Message = response.Message, Code = response.Code };
    }

    public async Task<ResponseDto> UpdatePropertyAsync(PropertyDto property)
    {
        var request = new PropertyRequest
        {
            PropertyId = property.Id,
            Title = property.Title,
            SortOrder = property.SortOrder,
            IsColor = property.IsColor,
            IsActive = property.IsActive
        };

        var response = await _client.UpdatePropertyAsync(request);

        return new() { Status = response.Status, Message = response.Message, Code = response.Code };
    }

    public async Task<ResponseDto> DeletePropertyAsync(int propertyId)
    {
        var request = new PropertyRequest { PropertyId = propertyId };
        var response = await _client.DeletePropertyAsync(request);

        return new() { Status = response.Status, Message = response.Message, Code = response.Code };
    }

    public async Task<List<CateguryDto>> GetAllCategoriesAsync()
    {
        var request = new EmptyRequest();
        var response = await _client.GetAllCategoriesAsync(request);

        var categories = new List<CateguryDto>();

        foreach (var category in response.Data)
        {
            categories.Add(new()
            {
                Id = category.Id,
                Title = category.Title,
                Image = category.Image,
                Slug = category.Slug,
                ParentId = category.ParentId,
                IsActive = category.IsActive,
                SortOrder = category.SortOrder
            });
        }

        return categories;
    }

    public async Task<ResponseDto> CreateOrUpdatePropertyCategoryAsync(int propertyId, int categoryId)
    {
        var request = new PropertyCategoryRequest { PropertyId = propertyId, CategoryId = categoryId };

        var response = await _client.CreateOrUpdatePropertyCategoryAsync(request);

        return new() { Status = response.Status, Message = response.Message, Code = response.Code };
    }

    private PropertyDto MapToPropertyDto(PropertyDto response)
    {
        var property = new PropertyDto
        {
            Id = response.Id,
            Title = response.Title,
            SortOrder = response.SortOrder,
            IsColor = response.IsColor,
            IsActive = response.IsActive,
            Categories = new List<CateguryDto>()
        };

        foreach (var category in response.Categories)
        {
            property.Categories.Add(new CateguryDto
            {
                Id = category.Id,
                Title = category.Title,
                Image = category.Image,
                Slug = category.Slug,
                ParentId = category.ParentId,
                IsActive = category.IsActive,
                SortOrder = category.SortOrder
            });
        }

        return property;
    }

    private PagingDto MapToPagingDto(PropertyPagingDto response)
    {
        var paging = new PagingDto
        {
            PageIndex = response.PageIndex,
            PageSize = response.PageSize,
            TotalPages = response.TotalPages,
            TotalCount = response.TotalCount,
            HasPreviousPage = response.HasPreviousPage,
            HasNextPage = response.HasNextPage,
            Data = new List<PropertyDto>()
        };

        foreach (var property in response.Data)
        {
            paging.Data.Add(MapToPropertyDto(property));
        }

        return paging;
    }
}