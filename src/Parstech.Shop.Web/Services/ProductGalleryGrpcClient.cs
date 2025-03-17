using Grpc.Net.Client;

namespace Parstech.Shop.Web.Services;

public class ProductGalleryGrpcClient
{
    private readonly ProductGalleryService.ProductGalleryServiceClient _client;

    public ProductGalleryGrpcClient(GrpcChannel channel)
    {
        _client = new ProductGalleryService.ProductGalleryServiceClient(channel);
    }

    public async Task<ProductGalleryResponse> GetProductGalleriesAsync(int productId)
    {
        var request = new ProductGalleryRequest { ProductId = productId };
        return await _client.GetProductGalleriesAsync(request);
    }
}