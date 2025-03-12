using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.Extensions.Configuration;
using Parstech.Shop.Shared.Protos.UserShipping;
using System.Net.Http;
using System.Threading.Tasks;

namespace Parstech.Shop.Web.Services.GrpcClients
{
    public class UserShippingGrpcClient : GrpcClientBase
    {
        private readonly UserShippingService.UserShippingServiceClient _client;

        public UserShippingGrpcClient(IConfiguration configuration) : base(configuration)
        {
            var channel = GrpcChannel.ForAddress(ApiServiceUrl, new GrpcChannelOptions
            {
                HttpHandler = new GrpcWebHandler(new HttpClientHandler())
            });

            _client = new UserShippingService.UserShippingServiceClient(channel);
        }

        public async Task<UserShipping> GetUserShippingAsync(int shippingId)
        {
            var request = new UserShippingRequest { ShippingId = shippingId };
            return await _client.GetUserShippingAsync(request);
        }

        public async Task<UserShippingListResponse> GetUserShippingAddressesAsync(string userName)
        {
            var request = new UserShippingListRequest { UserName = userName };
            return await _client.GetUserShippingAddressesAsync(request);
        }

        public async Task<UserShipping> CreateUserShippingAsync(CreateUserShippingRequest request)
        {
            return await _client.CreateUserShippingAsync(request);
        }

        public async Task<UserShipping> UpdateUserShippingAsync(UpdateUserShippingRequest request)
        {
            return await _client.UpdateUserShippingAsync(request);
        }

        public async Task<UserShippingResponse> DeleteUserShippingAsync(int shippingId)
        {
            var request = new UserShippingRequest { ShippingId = shippingId };
            return await _client.DeleteUserShippingAsync(request);
        }
    }
}
