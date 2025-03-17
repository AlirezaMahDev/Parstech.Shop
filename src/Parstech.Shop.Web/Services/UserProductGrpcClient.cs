using Grpc.Net.Client;

using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.Web.Services;

public class UserProductGrpcClient
{
    private readonly UserProductService.UserProductServiceClient _client;

    public UserProductGrpcClient(GrpcChannel channel)
    {
        _client = new UserProductService.UserProductServiceClient(channel);
    }

    public async Task<UserProductResponse> CreateUserProductAsync(string userName, int productId, string type)
    {
        var request = new CreateUserProductRequest { UserName = userName, ProductId = productId, Type = type };

        return await _client.CreateUserProductAsync(request);
    }

    public async Task<UserProductResponse> DeleteUserProductAsync(int userProductId)
    {
        var request = new DeleteUserProductRequest { UserProductId = userProductId };

        return await _client.DeleteUserProductAsync(request);
    }

    public async Task<IEnumerable<UserProduct>> GetUserProductsAsync(string userName, string type)
    {
        var request = new GetUserProductsRequest { UserName = userName, Type = type };

        var response = await _client.GetUserProductsAsync(request);
        return response.Products;
    }

    public async Task<IEnumerable<UserProduct>> GetUserFavoritesAsync(string userName)
    {
        return await GetUserProductsAsync(userName, "Favorite");
    }

    public async Task<IEnumerable<UserProduct>> GetUserComparisonsAsync(string userName)
    {
        return await GetUserProductsAsync(userName, "Compare");
    }
}