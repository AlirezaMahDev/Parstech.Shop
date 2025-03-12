using Parstech.Shop.Shared.Protos.Order;
using Google.Protobuf.WellKnownTypes;

namespace Parstech.Shop.Web.Services.GrpcClients
{
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

        public async Task<OrderResponse> CreateOrderAsync(string userName, List<int> productIds, int shippingAddressId, int billingAddressId, string? couponCode = null, int paymentTypeId = 1)
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
            var request = new UpdateOrderStatusRequest
            {
                OrderId = orderId,
                StatusId = statusId
            };

            return await _client.UpdateOrderStatusAsync(request);
        }

        public async Task<OrderFilter> GetOrderFiltersAsync()
        {
            var request = new OrderFiltersRequest();
            return await _client.GetOrderFiltersAsync(request);
        }
    }
}
