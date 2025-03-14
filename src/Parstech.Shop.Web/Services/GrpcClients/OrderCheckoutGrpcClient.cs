using Grpc.Net.Client;
using Parstech.Shop.Shared.Protos.OrderCheckout;

namespace Parstech.Shop.Web.Services.GrpcClients
{
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

        public async Task<ChangeOrderDetailResponse> ChangeOrderDetailAsync(int detailId, int count)
        {
            var request = new ChangeOrderDetailRequest
            {
                DetailId = detailId,
                Count = count
            };
            return await _client.ChangeOrderDetailAsync(request);
        }

        public async Task<DeleteOrderDetailResponse> DeleteOrderDetailAsync(int detailId)
        {
            var request = new DeleteOrderDetailRequest { DetailId = detailId };
            return await _client.DeleteOrderDetailAsync(request);
        }

        public async Task<CompleteOrderResponse> CompleteOrderAsync(int orderId, int orderShippingId, int payTypeId, int? transactionId, int month)
        {
            var request = new CompleteOrderRequest
            {
                OrderId = orderId,
                OrderShippingId = orderShippingId,
                PayTypeId = payTypeId,
                Month = month
            };
            
            if (transactionId.HasValue)
            {
                request.TransactionId = transactionId.Value;
            }
            
            return await _client.CompleteOrderAsync(request);
        }
    }
} 