using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.Web.Services.GrpcClients;

public class StoreAdminGrpcClient : IStoreAdminGrpcClient
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
        var request = new ContractOrderRequest { OrderId = orderId, StoreName = storeName ?? "All" };

        var response = await _client.GetContractForOrderAsync(request);

        return new ResponseDto
        {
            IsSuccessed = response.IsSuccessed, Message = response.Message, Object = response.ContractHtml
        };
    }

    /// <summary>
    /// Get a store by ID
    /// </summary>
    public async Task<UserStoreDto> GetStoreByIdAsync(int storeId)
    {
        var request = new StoreRequest { StoreId = storeId };
        var response = await _client.GetStoreByIdAsync(request);
        return MapToUserStoreDto(response);
    }

    /// <summary>
    /// Create a new store
    /// </summary>
    public async Task<ResponseDto> CreateStoreAsync(UserStoreDto storeDto)
    {
        var request = new UserStoreProtoDto
        {
            Name = storeDto.Name,
            LatinName = storeDto.LatinName,
            UserId = storeDto.UserId,
            Mobile = storeDto.Mobile,
            Logo = storeDto.Logo,
            Address = storeDto.Address,
            IsActive = storeDto.IsActive
        };

        var response = await _client.CreateStoreAsync(request);
        return new ResponseDto { IsSuccessed = response.Status, Message = response.Message, Code = response.Code };
    }

    /// <summary>
    /// Update an existing store
    /// </summary>
    public async Task<ResponseDto> UpdateStoreAsync(UserStoreDto storeDto)
    {
        var request = new UserStoreProtoDto
        {
            Id = storeDto.Id,
            Name = storeDto.Name,
            LatinName = storeDto.LatinName,
            UserId = storeDto.UserId,
            Mobile = storeDto.Mobile,
            Logo = storeDto.Logo,
            Address = storeDto.Address,
            IsActive = storeDto.IsActive
        };

        var response = await _client.UpdateStoreAsync(request);
        return new ResponseDto { IsSuccessed = response.Status, Message = response.Message, Code = response.Code };
    }

    /// <summary>
    /// Delete a store
    /// </summary>
    public async Task<ResponseDto> DeleteStoreAsync(int storeId)
    {
        var request = new StoreRequest { StoreId = storeId };
        var response = await _client.DeleteStoreAsync(request);
        return new ResponseDto { IsSuccessed = response.Status, Message = response.Message, Code = response.Code };
    }

    #region Mapping Methods

    private SalesPagingDto MapToSalesPagingDto(Shared.Protos.StoreAdmin.SalesPagingDto source)
    {
        return new SalesPagingDto
        {
            CurrentPage = source.CurrentPage,
            IsSuccessed = source.IsSuccessed,
            Message = source.Message,
            Sales = source.Sales.Select(s => MapToSalesDto(s)).ToList(),
            TakePage = source.TakePage,
            TotalRow = source.TotalRow
        };
    }

    private SalesDto MapToSalesDto(Shared.Protos.StoreAdmin.SalesDto source)
    {
        return new SalesDto
        {
            Id = source.Id,
            PayDate = source.PayDate,
            IsFinaly = source.IsFinaly,
            OrderCount = source.OrderCount,
            StoreName = source.StoreName,
            TotalPrice = source.TotalPrice,
            TotalPriceGold = source.TotalPriceGold,
            UserFullName = source.UserFullName,
            UserId = source.UserId
        };
    }

    private UserStoreDto MapToUserStoreDto(Shared.Protos.StoreAdmin.UserStoreDto source)
    {
        return new UserStoreDto
        {
            Id = source.Id,
            Name = source.Name,
            LatinName = source.LatinName,
            UserId = source.UserId,
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
            Title = source.Title,
            Description = source.Description,
            Status = source.Status,
            CreateDate = source.CreateDate
        };
    }

    #endregion
}