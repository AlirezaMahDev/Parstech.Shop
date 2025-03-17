using Grpc.Net.Client;

namespace Parstech.Shop.Web.Services.GrpcClients;

public class ShippingGrpcClient
{
    private readonly ShippingService.ShippingServiceClient _client;

    public ShippingGrpcClient(GrpcChannel channel)
    {
        _client = new ShippingService.ShippingServiceClient(channel);
    }

    public async Task<UserShippingResponse> GetFirstUserShippingAsync(int userId)
    {
        var request = new FirstShippingRequest { UserId = userId };
        return await _client.GetFirstUserShippingAsync(request);
    }

    public async Task<ChangeShippingResponse> ChangeOrderShippingAsync(string action,
        int userShippingId,
        int orderId,
        int shippingCost)
    {
        var request = new ChangeShippingRequest
        {
            Action = action, UserShippingId = userShippingId, OrderId = orderId, ShippingCost = shippingCost
        };

        return await _client.ChangeOrderShippingAsync(request);
    }

    public async Task<OrderShippingResponse> GetOrderShippingAsync(int orderId)
    {
        var request = new OrderShippingRequest { OrderId = orderId };
        return await _client.GetOrderShippingAsync(request);
    }
}