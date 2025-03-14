using Parstech.Shop.Shared.Protos.Product;

namespace Parstech.Shop.Web.Services.GrpcClients
{
    public class ProductGrpcClient : GrpcClientBase
    {
        private readonly ProductService.ProductServiceClient _client;

        public ProductGrpcClient(IConfiguration configuration) : base(configuration)
        {
            _client = new ProductService.ProductServiceClient(Channel);
        }

        public async Task<Product> GetProductAsync(int productId)
        {
            var request = new ProductRequest { ProductId = productId };
            return await _client.GetProductAsync(request);
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            var request = new ProductRequest { ProductId = productId };
            return await _client.GetProductAsync(request);
        }

        public async Task<ProductPaging> GetProductsAsync(ProductParameter parameter, string? userName = null)
        {
            var request = new ProductPagingRequest
            {
                Parameter = parameter,
                UserName = userName ?? string.Empty
            };
            
            return await _client.GetProductsAsync(request);
        }

        public async Task<ProductPaging> SearchProductsAsync(string filter, int take = 4)
        {
            var request = new ProductSearchParameter
            {
                Filter = filter,
                Take = take
            };
            
            return await _client.SearchProductsAsync(request);
        }

        public async Task<ProductDetailShow> GetProductDetailsAsync(int productId)
        {
            var request = new ProductRequest { ProductId = productId };
            return await _client.GetProductDetailsAsync(request);
        }

        public async Task<IntegratedProductsPagingResponse> GetIntegratedProductsPagingAsync(ProductSearchParameterRequest parameter, string? userName = null)
        {
            var request = new IntegratedProductsPagingRequest
            {
                Parameter = parameter,
                UserName = userName ?? string.Empty
            };
            
            return await _client.GetIntegratedProductsPagingAsync(request);
        }
        
        public async Task<List<ProductResponse>> GetProductsWithDiscountAsync(int take, int sectionId)
        {
            var request = new ProductsWithDiscountRequest
            {
                Take = take,
                SectionId = sectionId
            };
            
            var response = await _client.GetProductsWithDiscountAsync(request);
            return response.Products.ToList();
        }
        
        public async Task<ProductPageing> ProductPagingSearchOrStoreAsync(ProductSearchParameterRequest parameter)
        {
            return await _client.ProductPagingSearchOrStoreAsync(parameter);
        }
    }
}
