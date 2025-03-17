using Google.Protobuf.WellKnownTypes;
using Google.Protobuf;

using Parstech.Shop.ApiService.Application.DTOs;

using StackExchange.Redis;

namespace Parstech.Shop.Web.Services.GrpcClients;

public class OrderGrpcClient : GrpcClientBase
{
    private readonly OrderService.OrderServiceClient _client;

    public OrderGrpcClient(IConfiguration configuration) : base(configuration)
    {
        _client = new OrderService.OrderServiceClient(Channel);
    }

    public async Task<Order> GetOrderAsync(int orderId)
    {
        var request = new OrderRequest { OrderId = orderId };
        return await _client.GetOrderAsync(request);
    }

    public async Task<OrderDetailShow> GetOrderDetailsAsync(int orderId)
    {
        var request = new OrderRequest { OrderId = orderId };
        return await _client.GetOrderDetailsAsync(request);
    }

    public async Task<OrdersResponse> GetOrdersForUserAsync(string userName)
    {
        var request = new UserOrdersRequest { UserName = userName };
        return await _client.GetOrdersForUserAsync(request);
    }

    public async Task<OrderResponse> CreateOrderAsync(string userName,
        List<int> productIds,
        int shippingAddressId,
        int billingAddressId,
        string? couponCode = null,
        int paymentTypeId = 1)
    {
        var request = new CreateOrderRequest
        {
            UserName = userName,
            ShippingAddressId = shippingAddressId,
            BillingAddressId = billingAddressId,
            PaymentTypeId = paymentTypeId
        };

        // Add product IDs
        request.ProductIds.AddRange(productIds);

        // Add coupon code if provided
        if (!string.IsNullOrEmpty(couponCode))
        {
            request.CouponCode = new StringValue { Value = couponCode };
        }

        return await _client.CreateOrderAsync(request);
    }

    public async Task<OrderResponse> UpdateOrderStatusAsync(int orderId, int statusId)
    {
        var request = new UpdateOrderStatusRequest { OrderId = orderId, StatusId = statusId };

        return await _client.UpdateOrderStatusAsync(request);
    }

    public async Task<OrderFilter> GetOrderFiltersAsync(string? storeName = null)
    {
        var request = new OrderFiltersRequest { StoreName = storeName ?? string.Empty };
        return await _client.GetOrderFiltersAsync(request);
    }

    public async Task<PagingDto> GetOrdersPagingAsync(ParameterDto parameter)
    {
        return await _client.GetOrdersPagingAsync(parameter);
    }

    public async Task<OrderStatusesResponse> GetOrderStatusesAsync(int orderId)
    {
        var request = new OrderRequest { OrderId = orderId };
        return await _client.GetOrderStatusesAsync(request);
    }

    public async Task<OrderResponse> CreateOrderStatusAsync(OrderStatusDto orderStatus, byte[] fileData = null)
    {
        var request = new OrderStatusRequest { OrderStatus = orderStatus };

        if (fileData != null && fileData.Length > 0)
        {
            request.FileData = ByteString.CopyFrom(fileData);
        }

        return await _client.CreateOrderStatusAsync(request);
    }

    public async Task<OrderResponse> ChangeOrderShippingAsync(OrderShippingChangeRequest request)
    {
        return await _client.ChangeOrderShippingAsync(request);
    }

    public async Task<OrderWordFileResponse> GenerateOrderWordFileAsync(int orderId)
    {
        var request = new OrderRequest { OrderId = orderId };
        return await _client.GenerateOrderWordFileAsync(request);
    }

    public async Task<OrderResponse> CompleteOrderByAdminAsync(int orderId, string typeName, int? month = null)
    {
        var request = new CompleteOrderRequest { OrderId = orderId, TypeName = typeName ?? string.Empty };

        if (month.HasValue)
        {
            request.Month = new Int32Value { Value = month.Value };
        }

        return await _client.CompleteOrderByAdminAsync(request);
    }

    public async Task<OrderPaysResponse> GetOrderPaysAsync(int orderId)
    {
        var request = new OrderRequest { OrderId = orderId };
        return await _client.GetOrderPaysAsync(request);
    }

    public async Task<OrderResponse> AddOrderPayAsync(OrderPayDto orderPay)
    {
        var request = new OrderPayRequest { OrderPay = orderPay };
        return await _client.AddOrderPayAsync(request);
    }

    public async Task<OrderResponse> DeleteOrderPayAsync(int id)
    {
        var request = new OrderPayDeleteRequest { Id = id };
        return await _client.DeleteOrderPayAsync(request);
    }
}