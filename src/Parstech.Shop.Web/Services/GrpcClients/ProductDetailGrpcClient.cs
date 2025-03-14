using Grpc.Net.Client;
using Parstech.Shop.Shared.Protos.ProductDetail;

namespace Parstech.Shop.Web.Services.GrpcClients
{
    public class ProductDetailGrpcClient
    {
        private readonly ProductDetailService.ProductDetailServiceClient _client;

        public ProductDetailGrpcClient(GrpcChannel channel)
        {
            _client = new ProductDetailService.ProductDetailServiceClient(channel);
        }

        public async Task<ProductDetailResponse> GetProductByShortLinkAsync(string shortLink, int storeId, string userName = null)
        {
            var request = new ProductDetailRequest
            {
                ShortLink = shortLink,
                StoreId = storeId
            };
            
            if (!string.IsNullOrEmpty(userName))
            {
                request.UserName = userName;
            }
            
            return await _client.GetProductByShortLinkAsync(request);
        }
    }
} 