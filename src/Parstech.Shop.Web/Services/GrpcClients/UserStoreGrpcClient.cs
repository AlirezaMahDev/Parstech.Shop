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
    }
} 