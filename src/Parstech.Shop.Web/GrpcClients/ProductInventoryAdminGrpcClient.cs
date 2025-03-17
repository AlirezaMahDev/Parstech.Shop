using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.Web.GrpcClients;

public class ProductInventoryAdminGrpcClient : IProductInventoryAdminGrpcClient
{
    private readonly ProductInventoryAdminService.ProductInventoryAdminServiceClient _client;

    public ProductInventoryAdminGrpcClient(ProductInventoryAdminService.ProductInventoryAdminServiceClient client)
    {
        _client = client;
    }

    public async Task<ProductStockPriceResponse> GetProductStockPriceAsync(int stockPriceId)
    {
        var request = new ProductStockPriceRequest { StockPrice = new ProductStockPriceDto { Id = stockPriceId } };
        return await _client.GetProductStockPriceAsync(request);
    }

    public async Task<ProductStockPriceListResponse> GetProductStockPricesAsync(int productId)
    {
        var request = new ProductIdRequest { ProductId = productId };
        return await _client.GetProductStockPricesAsync(request);
    }

    public async Task<ResponseDto> CreateProductStockPriceAsync(ProductStockPriceDto stockPrice, string userName = null)
    {
        var request = new ProductStockPriceRequest { StockPrice = stockPrice, UserName = userName ?? string.Empty };
        return await _client.CreateProductStockPriceAsync(request);
    }

    public async Task<ResponseDto> UpdateProductStockPriceAsync(ProductStockPriceDto stockPrice, string userName = null)
    {
        var request = new ProductStockPriceRequest { StockPrice = stockPrice, UserName = userName ?? string.Empty };
        return await _client.UpdateProductStockPriceAsync(request);
    }

    public async Task<ResponseDto> DeleteProductStockPriceAsync(int stockPriceId)
    {
        var request = new ProductStockPriceIdRequest { StockPriceId = stockPriceId };
        return await _client.DeleteProductStockPriceAsync(request);
    }

    public async Task<ResponseDto> QuickEditProductStockPriceAsync(int id,
        double price,
        double salePrice,
        int quantity,
        string userName = null)
    {
        var request = new QuickEditRequest
        {
            Id = id,
            Price = price,
            SalePrice = salePrice,
            Quantity = quantity,
            UserName = userName ?? string.Empty
        };
        return await _client.QuickEditProductStockPriceAsync(request);
    }

    public async Task<ProductStockPriceListResponse> GetProductInventoryByStoreAsync(int storeId)
    {
        var request = new StoreIdRequest { StoreId = storeId };
        return await _client.GetProductInventoryByStoreAsync(request);
    }

    public async Task<StoreInventorySummaryResponse> GetStoreInventorySummaryAsync(int storeId)
    {
        var request = new StoreIdRequest { StoreId = storeId };
        return await _client.GetStoreInventorySummaryAsync(request);
    }
}