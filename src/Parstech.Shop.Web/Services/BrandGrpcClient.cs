using Grpc.Net.Client;

using Parstech.Shop.Shared.Protos.Brand;
using Parstech.Shop.Shared.Protos.Product;

namespace Parstech.Shop.Web.Services;

public class BrandGrpcClient
{
    private readonly BrandService.BrandServiceClient _client;

    public BrandGrpcClient(GrpcChannel channel)
    {
        _client = new BrandService.BrandServiceClient(channel);
    }

    public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
    {
        var request = new GetBrandsRequest();
        var response = await _client.GetBrandsAsync(request);
        return response.Brands;
    }

    public async Task<BrandsResponse> GetAllBrandsAsync()
    {
        var request = new GetBrandsRequest();
        return await _client.GetBrandsAsync(request);
    }

    public async Task<BrandDto> GetBrandByIdAsync(int id)
    {
        var request = new GetBrandRequest { BrandId = id };
        var response = await _client.GetBrandAsync(request);
        return response.Brand;
    }
}