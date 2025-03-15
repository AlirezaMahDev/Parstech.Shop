using Parstech.Shop.Shared.Protos.StoreAdmin;
using Shop.Application.DTOs.OrderDetail;
using Shop.Application.DTOs.OrderStatus;
using Shop.Application.DTOs.Response;
using Shop.Application.DTOs.UserStore;

namespace Parstech.Shop.Web.Services.GrpcClients
{
    public class StoreAdminGrpcClient
    {
        private readonly StoreAdminService.StoreAdminServiceClient _client;

        public StoreAdminGrpcClient(StoreAdminService.StoreAdminServiceClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Get sales data for store
        /// </summary>
        public async Task<SalesPagingDto> GetSalesForStoreAsync(SalesParameterDto parameters, bool isAdmin)
        {
            var request = new SalesParameterRequest
            {
                CurrentPage = parameters.CurrentPage,
                TakePage = parameters.TakePage,
                Filter = parameters.Filter ?? string.Empty,
                FromDate = parameters.FromDate ?? string.Empty,
                ToDate = parameters.ToDate ?? string.Empty,
                StoreId = parameters.StoreId,
                IsAdmin = isAdmin
            };

            var response = await _client.GetSalesForStoreAsync(request);
            return MapToSalesPagingDto(response);
        }

        /// <summary>
        /// Get all user stores
        /// </summary>
        public async Task<List<UserStoreDto>> GetUserStoresAsync()
        {
            var request = new EmptyRequest();
            var response = await _client.GetUserStoresAsync(request);
            
            var result = new List<UserStoreDto>();
            foreach (var store in response.Stores)
            {
                result.Add(MapToUserStoreDto(store));
            }
            
            return result;
        }

        /// <summary>
        /// Get order statuses by order ID
        /// </summary>
        public async Task<List<OrderStatusDto>> GetOrderStatusesAsync(int orderId)
        {
            var request = new OrderStatusRequest { OrderId = orderId };
            var response = await _client.GetOrderStatusesAsync(request);
            
            var result = new List<OrderStatusDto>();
            foreach (var status in response.Statuses)
            {
                result.Add(MapToOrderStatusDto(status));
            }
            
            return result;
        }

        /// <summary>
        /// Get contract for order
        /// </summary>
        public async Task<ResponseDto> GetContractForOrderAsync(int orderId, string storeName)
        {
            var request = new ContractOrderRequest 
            { 
                OrderId = orderId,
                StoreName = storeName ?? "All" 
            };
            
            var response = await _client.GetContractForOrderAsync(request);
            
            return new ResponseDto
            {
                IsSuccessed = response.IsSuccessed,
                Message = response.Message,
                Object = response.ContractHtml
            };
        }

        #region Mapping Methods

        private SalesPagingDto MapToSalesPagingDto(Shared.Protos.StoreAdmin.SalesPagingDto source)
        {
            var result = new SalesPagingDto
            {
                CurrentPage = source.CurrentPage,
                PageCount = source.PageCount,
                RowCount = source.RowCount,
                List = new List<SalesDto>(),
                StoresSelect = new UserStoreDto[source.StoresSelect.Count]
            };

            for (int i = 0; i < source.List.Count; i++)
            {
                result.List.Add(MapToSalesDto(source.List[i]));
            }

            for (int i = 0; i < source.StoresSelect.Count; i++)
            {
                result.StoresSelect[i] = MapToUserStoreDto(source.StoresSelect[i]);
            }

            return result;
        }

        private SalesDto MapToSalesDto(Shared.Protos.StoreAdmin.SalesDto source)
        {
            return new SalesDto
            {
                Id = source.Id,
                OrderId = source.OrderId,
                OrderNumber = source.OrderNumber,
                ProductName = source.ProductName,
                ProductId = source.ProductId,
                Count = source.Count,
                Price = source.Price,
                SumPrice = source.SumPrice,
                StoreId = source.StoreId,
                StoreName = source.StoreName,
                LatinStoreName = source.LatinStoreName,
                UserName = source.UserName,
                FullName = source.FullName,
                CreateDate = source.CreateDate?.ToDateTime(),
                CreateDateShamsi = source.CreateDateShamsi,
                OrderStatusId = source.OrderStatusId,
                OrderStatusTitle = source.OrderStatusTitle
            };
        }

        private UserStoreDto MapToUserStoreDto(Shared.Protos.StoreAdmin.UserStoreDto source)
        {
            return new UserStoreDto
            {
                Id = source.Id,
                UserId = source.UserId,
                Name = source.Name,
                LatinName = source.LatinName,
                Mobile = source.Mobile,
                Logo = source.Logo,
                Address = source.Address,
                IsActive = source.IsActive
            };
        }

        private OrderStatusDto MapToOrderStatusDto(Shared.Protos.StoreAdmin.OrderStatusDto source)
        {
            return new OrderStatusDto
            {
                Id = source.Id,
                OrderId = source.OrderId,
                StatusId = source.StatusId,
                StatusTitle = source.StatusTitle,
                Description = source.Description,
                File = source.File,
                CreateDate = source.CreateDate?.ToDateTime(),
                CreateDateShamsi = source.CreateDateShamsi
            };
        }

        #endregion
    }
} 