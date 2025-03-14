using Grpc.Net.Client;
using Parstech.Shop.Shared.Protos.Brand;

namespace Parstech.Shop.Web.Services.GrpcClients
{
    public class BrandGrpcClient
    {
        private readonly BrandService.BrandServiceClient _client;

        public BrandGrpcClient(GrpcChannel channel)
        {
            _client = new BrandService.BrandServiceClient(channel);
        }

        public async Task<IEnumerable<Brand>> GetBrandsAsync()
        {
            var request = new BrandsRequest();
            var response = await _client.GetBrandsAsync(request);
            return response.Brands;
        }

        public async Task<Brand> GetBrandByIdAsync(int id)
        {
            var request = new BrandByIdRequest { Id = id };
            return await _client.GetBrandByIdAsync(request);
        }
    }
} 