using Shop.Application.DTOs.OrderStatus;
using Shop.Application.DTOs.Response;
using Shop.Application.DTOs.UserStore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parstech.Shop.Web.Services.GrpcClients
{
    public interface IStoreAdminGrpcClient
    {
        /// <summary>
        /// Get sales data for store
        /// </summary>
        Task<SalesPagingDto> GetSalesForStoreAsync(SalesParameterDto parameters, bool isAdmin);

        /// <summary>
        /// Get all user stores
        /// </summary>
        Task<List<UserStoreDto>> GetUserStoresAsync();

        /// <summary>
        /// Get order statuses by order ID
        /// </summary>
        Task<List<OrderStatusDto>> GetOrderStatusesAsync(int orderId);

        /// <summary>
        /// Get contract for order
        /// </summary>
        Task<ResponseDto> GetContractForOrderAsync(int orderId, string storeName);
        
        /// <summary>
        /// Get a store by ID
        /// </summary>
        Task<UserStoreDto> GetStoreByIdAsync(int storeId);
        
        /// <summary>
        /// Create a new store
        /// </summary>
        Task<ResponseDto> CreateStoreAsync(UserStoreDto storeDto);
        
        /// <summary>
        /// Update an existing store
        /// </summary>
        Task<ResponseDto> UpdateStoreAsync(UserStoreDto storeDto);
        
        /// <summary>
        /// Delete a store
        /// </summary>
        Task<ResponseDto> DeleteStoreAsync(int storeId);
    }
} 