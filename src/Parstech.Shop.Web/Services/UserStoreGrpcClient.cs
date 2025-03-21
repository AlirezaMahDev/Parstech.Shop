using Grpc.Net.Client;

using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.Web.Services;

public class UserStoreGrpcClient
{
    private readonly UserStoreService.UserStoreServiceClient _client;

    public UserStoreGrpcClient(GrpcChannel channel)
    {
        _client = new UserStoreService.UserStoreServiceClient(channel);
    }

    public async Task<IEnumerable<UserStore>> GetStoresAsync()
    {
        var request = new StoresRequest();
        var response = await _client.GetStoresAsync(request);
        return response.Stores;
    }

    public async Task<UserStore> GetStoreByIdAsync(int id)
    {
        var request = new StoreByIdRequest { Id = id };
        return await _client.GetStoreByIdAsync(request);
    }

    public async Task<UserStore> GetStoreByLatinNameAsync(string latinName)
    {
        var request = new StoreByLatinNameRequest { LatinName = latinName };
        return await _client.GetStoreByLatinNameAsync(request);
    }
}