using Grpc.Net.Client;

namespace Parstech.Shop.Web.Services;

public class CategoryGrpcClient
{
    private readonly CategoryService.CategoryServiceClient _client;

    public CategoryGrpcClient(GrpcChannel channel)
    {
        _client = new CategoryService.CategoryServiceClient(channel);
    }

    public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
    {
        var request = new GetCategoriesRequest();
        var response = await _client.GetCategoriesAsync(request);
        return response.Categories;
    }

    public async Task<CategoryTreeResponse> GetCategoryTreeAsync(long? rootCategoryId = null, int depth = 3)
    {
        var request = new GetCategoryTreeRequest
        {
            Depth = depth,
            IncludeProductCounts = true
        };
        
        if (rootCategoryId.HasValue)
        {
            request.RootCategoryId = rootCategoryId.Value;
        }
        
        return await _client.GetCategoryTreeAsync(request);
    }

    public async Task<CategoryDto> GetCategoryByIdAsync(long categoryId)
    {
        var request = new GetCategoryRequest
        {
            CategoryId = categoryId,
            IncludeChildren = true
        };
        var response = await _client.GetCategoryAsync(request);
        return response.Category;
    }

    public async Task<CategoryDto> GetCategoryBySlugAsync(string slug)
    {
        var request = new GetCategoryRequest
        {
            Slug = slug,
            IncludeChildren = true,
            IncludeParent = true
        };
        var response = await _client.GetCategoryAsync(request);
        return response.Category;
    }

    public async Task<CategoryPathResponse> GetCategoryPathAsync(long categoryId)
    {
        var request = new GetCategoryPathRequest { CategoryId = categoryId };
        return await _client.GetCategoryPathAsync(request);
    }
}