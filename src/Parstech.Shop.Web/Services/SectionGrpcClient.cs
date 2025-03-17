using System.Collections.Specialized;

using Grpc.Net.Client;

using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.Web.Services;

public class SectionGrpcClient
{
    private readonly SectionService.SectionServiceClient _client;

    public SectionGrpcClient(GrpcChannel channel)
    {
        _client = new SectionService.SectionServiceClient(channel);
    }

    public async Task<IEnumerable<BitVector32.Section>> GetSectionsAsync(int? parentId = null)
    {
        var request = new SectionRequest();
        if (parentId.HasValue)
        {
            request.ParentId = parentId.Value;
        }

        var response = await _client.GetSectionsAsync(request);
        return response.Sections;
    }

    public async Task<IEnumerable<SectionDetail>> GetSectionDetailsAsync(int sectionId)
    {
        var request = new SectionDetailsRequest { SectionId = sectionId };
        var response = await _client.GetSectionDetailsAsync(request);
        return response.Details;
    }

    public async Task<SectionWithDetailsResponse> GetSectionAndDetailsByIdAsync(int sectionId)
    {
        var request = new SectionByIdRequest { SectionId = sectionId };
        return await _client.GetSectionAndDetailsByIdAsync(request);
    }

    public async Task<SectionWithDetailsResponse> GetSectionAndDetailsByStoreAsync(string store)
    {
        var request = new SectionByStoreRequest { Store = store };
        return await _client.GetSectionAndDetailsByStoreAsync(request);
    }
}