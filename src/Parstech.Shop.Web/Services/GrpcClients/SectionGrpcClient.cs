using Grpc.Net.Client;
using Parstech.Shop.Shared.Protos.Section;

namespace Parstech.Shop.Web.Services.GrpcClients
{
    public class SectionGrpcClient
    {
        private readonly SectionService.SectionServiceClient _client;

        public SectionGrpcClient(GrpcChannel channel)
        {
            _client = new SectionService.SectionServiceClient(channel);
        }

        public async Task<SectionResponse> GetSectionsAsync(SectionRequest request)
        {
            return await _client.GetSectionsAsync(request);
        }

        public async Task<SectionDetailsResponse> GetSectionDetailsAsync(SectionDetailsRequest request)
        {
            return await _client.GetSectionDetailsAsync(request);
        }
    }
} 