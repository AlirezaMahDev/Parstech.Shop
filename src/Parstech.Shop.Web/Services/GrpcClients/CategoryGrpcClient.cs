using Grpc.Net.Client;
using Parstech.Shop.Shared.Protos.Category;

namespace Parstech.Shop.Web.Services.GrpcClients
{
    public class CategoryGrpcClient
    {
        private readonly CategoryService.CategoryServiceClient _client;

        public CategoryGrpcClient(GrpcChannel channel)
        {
            _client = new CategoryService.CategoryServiceClient(channel);
        }

        public async Task<IEnumerable<Category>> GetParentCategoriesAsync()
        {
            var request = new ParentCategoriesRequest();
            var response = await _client.GetParentCategoriesAsync(request);
            return response.Categories;
        }

        public async Task<IEnumerable<Category>> GetSubCategoriesAsync(int parentId)
        {
            var request = new SubCategoriesRequest { ParentId = parentId };
            var response = await _client.GetSubCategoriesAsync(request);
            return response.Categories;
        }

        public async Task<Category> GetCategoryByLatinNameAsync(string latinName)
        {
            var request = new CategoryByLatinNameRequest { LatinName = latinName };
            return await _client.GetCategoryByLatinNameAsync(request);
        }
        
        public async Task<CategoriesMenuResponse> GetCategoriesMenuAsync()
        {
            var request = new CategoriesMenuRequest();
            return await _client.GetCategoriesMenuAsync(request);
        }
    }
} 