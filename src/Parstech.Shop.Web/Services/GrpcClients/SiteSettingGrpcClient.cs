using Grpc.Net.Client;
using Parstech.Shop.Shared.Protos.SiteSetting;

namespace Parstech.Shop.Web.Services.GrpcClients
{
    public class SiteSettingGrpcClient
    {
        private readonly SiteSettingService.SiteSettingServiceClient _client;

        public SiteSettingGrpcClient(GrpcChannel channel)
        {
            _client = new SiteSettingService.SiteSettingServiceClient(channel);
        }
        
        public async Task<SettingAndSeoResponse> GetSettingAndSeoAsync()
        {
            var request = new SettingAndSeoRequest();
            return await _client.GetSettingAndSeoAsync(request);
        }
    }
} 