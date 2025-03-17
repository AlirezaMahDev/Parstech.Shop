using Grpc.Net.Client;

namespace Parstech.Shop.Web.Services.GrpcClients;

public class TorobGrpcClient
{
    private readonly TorobService.TorobServiceClient _client;

    public TorobGrpcClient(GrpcChannel channel)
    {
        _client = new TorobService.TorobServiceClient(channel);
    }

    public async Task<Torob> GetTorobProductAsync(int storeId, string baseUrl)
    {
        var request = new TorobRequest { StoreId = storeId, BaseUrl = baseUrl };

        return await _client.GetTorobProductAsync(request);
    }

    public async Task<IEnumerable<TorobProduct>> GetTorobProductsAsync(int page)
    {
        var request = new TorobProductsRequest { Page = page };
        var response = await _client.GetTorobProductsAsync(request);
        return response.Products;
    }
}