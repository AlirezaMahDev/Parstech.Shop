using Grpc.Net.Client;
using Parstech.Shop.Shared.Protos.UserStore;

namespace Parstech.Shop.Web.Services.GrpcClients
{
    public class UserStoreGrpcClient
    {
        private readonly UserStoreService.UserStoreServiceClient _client;

        public UserStoreGrpcClient(GrpcChannel channel)
        {
            _client = new UserStoreService.UserStoreServiceClient(channel);
        }

        public async Task<UserStoreResponse> GetStoresAsync()
        {
            return await _client.GetStoresAsync(new StoresRequest());
        }

        public async Task<UserStore> GetStoreByIdAsync(int id)
        {
            return await _client.GetStoreByIdAsync(new StoreByIdRequest { Id = id });
        }
    }
} 