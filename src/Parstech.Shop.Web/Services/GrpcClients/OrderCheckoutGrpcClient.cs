using Grpc.Net.Client;

namespace Parstech.Shop.Web.Services.GrpcClients;

public class OrderCheckoutGrpcClient
{
    private readonly OrderCheckoutService.OrderCheckoutServiceClient _client;

    public OrderCheckoutGrpcClient(GrpcChannel channel)
    {
        _client = new OrderCheckoutService.OrderCheckoutServiceClient(channel);
    }

    public async Task<OrderResponse> GetOpenOrderAsync(string userName)
    {
        var request = new OpenOrderRequest { UserName = userName };
        return await _client.GetOpenOrderAsync(request);
    }

    public async Task<RefreshOrderResponse> RefreshOrderAsync(int orderId)
    {
        var request = new RefreshOrderRequest { OrderId = orderId };
        return await _client.RefreshOrderAsync(request);
    }

    public async Task<OrderDetailsResponse> GetOrderDetailsAsync(int orderId)
    {
        var request = new OrderDetailsRequest { OrderId = orderId };
        return await _client.GetOrderDetailsAsync(request);
    }

    public async Task<ChangeOrderDetailResponse> ChangeOrderDetailAsync(int orderDetailId, int quantity)
    {
        var request = new ChangeOrderDetailRequest { OrderDetailId = orderDetailId, Quantity = quantity };
        return await _client.ChangeOrderDetailAsync(request);
    }

    public async Task<DeleteOrderDetailResponse> DeleteOrderDetailAsync(int orderDetailId)
    {
        var request = new DeleteOrderDetailRequest { OrderDetailId = orderDetailId };
        return await _client.DeleteOrderDetailAsync(request);
    }

    public async Task<CompleteOrderResponse> CompleteOrderAsync(
        int orderId,
        int shippingId,
        int paymentTypeId,
        int? transactionId = null,
        string trackingCode = null)
    {
        var request = new CompleteOrderRequest
        {
            OrderId = orderId, ShippingId = shippingId, PaymentTypeId = paymentTypeId
        };

        if (transactionId.HasValue)
        {
            request.TransactionId = transactionId.Value;
        }

        if (!string.IsNullOrEmpty(trackingCode))
        {
            request.TrackingCode = trackingCode;
        }

        return await _client.CompleteOrderAsync(request);
    }

    public async Task<OrderPaymentsResponse> GetOrderPaymentsAsync(int orderId)
    {
        var request = new OrderPaymentsRequest { OrderId = orderId };
        return await _client.GetOrderPaymentsAsync(request);
    }

    public async Task<MultiplePaymentsResponse> CompleteOrderWithMultiplePaymentsAsync(
        int orderId,
        int paymentTypeId,
        double amount)
    {
        var request = new MultiplePaymentsRequest { OrderId = orderId, PaymentTypeId = paymentTypeId, Amount = amount };

        return await _client.CompleteOrderWithMultiplePaymentsAsync(request);
    }
}