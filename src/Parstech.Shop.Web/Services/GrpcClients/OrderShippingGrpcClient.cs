using Grpc.Net.Client;
using Grpc.Net.Client.Web;

namespace Parstech.Shop.Web.Services.GrpcClients;

public class OrderShippingGrpcClient : GrpcClientBase
{
    private readonly OrderShippingService.OrderShippingServiceClient _client;

    public OrderShippingGrpcClient(IConfiguration configuration) : base(configuration)
    {
        GrpcChannel? channel = GrpcChannel.ForAddress(ApiServiceUrl,
            new GrpcChannelOptions { HttpHandler = new GrpcWebHandler(new HttpClientHandler()) });

        _client = new OrderShippingService.OrderShippingServiceClient(channel);
    }

    public async Task<OrderShipping> GetOrderShippingAsync(int shippingId)
    {
        var request = new OrderShippingRequest { ShippingId = shippingId };
        return await _client.GetOrderShippingAsync(request);
    }

    public async Task<OrderShippingListResponse> GetOrderShippingsByOrderIdAsync(int orderId)
    {
        var request = new OrderShippingsByOrderRequest { OrderId = orderId };
        return await _client.GetOrderShippingsByOrderIdAsync(request);
    }

    public async Task<OrderShipping> CreateOrderShippingAsync(CreateOrderShippingRequest request)
    {
        return await _client.CreateOrderShippingAsync(request);
    }

    public async Task<OrderShipping> UpdateOrderShippingAsync(UpdateOrderShippingRequest request)
    {
        return await _client.UpdateOrderShippingAsync(request);
    }

    public async Task<OrderShippingResponse> DeleteOrderShippingAsync(int shippingId)
    {
        var request = new OrderShippingRequest { ShippingId = shippingId };
        return await _client.DeleteOrderShippingAsync(request);
    }

    public async Task<OrderShippingResponse> ChangeOrderShippingAsync(ChangeOrderShippingRequest request)
    {
        return await _client.ChangeOrderShippingAsync(request);
    }
}