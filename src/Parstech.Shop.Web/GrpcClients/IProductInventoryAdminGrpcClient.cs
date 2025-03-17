using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.Web.GrpcClients;

public interface IProductInventoryAdminGrpcClient
{
    Task<ProductStockPriceResponse> GetProductStockPriceAsync(int stockPriceId);
    Task<ProductStockPriceListResponse> GetProductStockPricesAsync(int productId);
    Task<ResponseDto> CreateProductStockPriceAsync(ProductStockPriceDto stockPrice, string userName = null);
    Task<ResponseDto> UpdateProductStockPriceAsync(ProductStockPriceDto stockPrice, string userName = null);
    Task<ResponseDto> DeleteProductStockPriceAsync(int stockPriceId);

    Task<ResponseDto> QuickEditProductStockPriceAsync(int id,
        double price,
        double salePrice,
        int quantity,
        string userName = null);

    Task<ProductStockPriceListResponse> GetProductInventoryByStoreAsync(int storeId);
    Task<StoreInventorySummaryResponse> GetStoreInventorySummaryAsync(int storeId);
}