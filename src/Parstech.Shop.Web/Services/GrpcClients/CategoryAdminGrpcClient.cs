using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.Web.Services.GrpcClients;

public class CategoryAdminGrpcClient : GrpcClientBase
{
    private readonly CategoryAdminService.CategoryAdminServiceClient _client;

    public CategoryAdminGrpcClient(IConfiguration configuration) : base(configuration)
    {
        _client = new CategoryAdminService.CategoryAdminServiceClient(Channel);
    }

    // Category management operations
    public async Task<CategoryPageingDto> GetCategoriesForAdminAsync(int currentPage, int takePage, string filter = "")
    {
        var request = new CategoryParameterRequest
        {
            CurrentPage = currentPage, TakePage = takePage, Filter = filter ?? string.Empty
        };

        return await _client.GetCategoriesForAdminAsync(request);
    }

    public async Task<CategoryDto> GetCategoryAsync(int categoryId)
    {
        var request = new CategoryRequest { CategoryId = categoryId };
        return await _client.GetCategoryAsync(request);
    }

    public async Task<CategoryListResponse> GetCategoryParentsAsync()
    {
        var request = new EmptyRequest();
        return await _client.GetCategoryParentsAsync(request);
    }

    public async Task<CategoryListResponse> GetAllCategoriesAsync(string filter = "")
    {
        var request = new CategoryFilterRequest { Filter = filter ?? string.Empty };
        return await _client.GetAllCategoriesAsync(request);
    }

    public async Task<ResponseDto> CreateCategoryAsync(CategoryDto category)
    {
        return await _client.CreateCategoryAsync(category);
    }

    public async Task<ResponseDto> UpdateCategoryAsync(CategoryDto category)
    {
        return await _client.UpdateCategoryAsync(category);
    }

    public async Task<ResponseDto> DeleteCategoryAsync(int categoryId)
    {
        var request = new CategoryRequest { CategoryId = categoryId };
        return await _client.DeleteCategoryAsync(request);
    }
}