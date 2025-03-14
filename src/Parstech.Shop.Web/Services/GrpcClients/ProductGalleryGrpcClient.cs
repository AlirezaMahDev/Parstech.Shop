using Grpc.Net.Client;
using Parstech.Shop.Shared.Protos.ProductGallery;

namespace Parstech.Shop.Web.Services.GrpcClients
{
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
} 