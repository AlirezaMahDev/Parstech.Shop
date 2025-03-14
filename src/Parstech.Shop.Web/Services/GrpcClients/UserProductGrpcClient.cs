using Grpc.Net.Client;
using Parstech.Shop.Shared.Protos.UserProduct;

namespace Parstech.Shop.Web.Services.GrpcClients
{
    public class UserProductGrpcClient
    {
        private readonly UserProductService.UserProductServiceClient _client;

        public UserProductGrpcClient(GrpcChannel channel)
        {
            _client = new UserProductService.UserProductServiceClient(channel);
        }

        public async Task<UserProductResponse> CreateUserProductAsync(CreateUserProductRequest request)
        {
            return await _client.CreateUserProductAsync(request);
        }

        public async Task<UserProductResponse> DeleteUserProductAsync(DeleteUserProductRequest request)
        {
            return await _client.DeleteUserProductAsync(request);
        }

        public async Task<GetUserProductsResponse> GetUserProductsAsync(GetUserProductsRequest request)
        {
            return await _client.GetUserProductsAsync(request);
        }
    }
} 