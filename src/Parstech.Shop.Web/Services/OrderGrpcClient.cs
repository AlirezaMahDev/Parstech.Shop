using Parstech.Shop.Shared.Protos.Order;

namespace Parstech.Shop.Web.Services;

public class OrderGrpcClient : GrpcClientBase
{
    private readonly OrderService.OrderServiceClient _orderService;
    private readonly OrderCheckoutService.OrderCheckoutServiceClient _checkoutService;
    private readonly PaymentService.PaymentServiceClient _paymentService;
    private readonly OrderShippingService.OrderShippingServiceClient _shippingService;

    public OrderGrpcClient(IConfiguration configuration) : base(configuration)
    {
        _orderService = new OrderService.OrderServiceClient(Channel);
        _checkoutService = new OrderCheckoutService.OrderCheckoutServiceClient(Channel);
        _paymentService = new PaymentService.PaymentServiceClient(Channel);
        _shippingService = new OrderShippingService.OrderShippingServiceClient(Channel);
    }

    // OrderService methods
    public async Task<OrderResponse> GetOrderAsync(long orderId)
    {
        var request = new OrderRequest { OrderId = orderId };
        return await _orderService.GetOrderAsync(request);
    }

    public async Task<OrderDetailsResponse> GetOrderDetailsAsync(long orderId)
    {
        var request = new OrderRequest { OrderId = orderId };
        return await _orderService.GetOrderDetailsAsync(request);
    }

    public async Task<UserOrdersResponse> GetOrdersByUserAsync(long userId, int page = 1, int pageSize = 10)
    {
        var request = new UserOrdersRequest 
        { 
            UserId = userId,
            Page = page,
            PageSize = pageSize
        };
        return await _orderService.GetOrdersByUserAsync(request);
    }

    public async Task<TrackOrderResponse> TrackOrderAsync(string orderNumber)
    {
        var request = new TrackOrderRequest { OrderNumber = orderNumber };
        return await _orderService.TrackOrderAsync(request);
    }

    public async Task<CancelOrderResponse> CancelOrderAsync(string orderNumber, long userId, string reason)
    {
        var request = new CancelOrderRequest 
        { 
            OrderNumber = orderNumber,
            UserId = userId,
            Reason = reason
        };
        return await _orderService.CancelOrderAsync(request);
    }

    public async Task<ReturnOrderResponse> ReturnOrderAsync(string orderNumber, long userId, List<ReturnItemDto> items, string reason)
    {
        var request = new ReturnOrderRequest
        {
            OrderNumber = orderNumber,
            UserId = userId,
            Reason = reason
        };
        request.ReturnItems.AddRange(items);
        return await _orderService.ReturnOrderAsync(request);
    }

    // OrderCheckoutService methods
    public async Task<CheckoutResponse> InitiateCheckoutAsync(List<CartItemDto> items, long userId)
    {
        var request = new InitiateCheckoutRequest
        {
            UserId = userId
        };
        request.CartItems.AddRange(items);
        return await _checkoutService.InitiateCheckoutAsync(request);
    }

    public async Task<CheckoutResponse> UpdateCheckoutAddressAsync(string checkoutId, AddressDto shippingAddress, AddressDto billingAddress, bool sameAsBilling = false)
    {
        var request = new CheckoutAddressRequest
        {
            CheckoutId = checkoutId,
            ShippingAddress = shippingAddress,
            BillingAddress = billingAddress,
            SameAsBilling = sameAsBilling
        };
        return await _checkoutService.UpdateCheckoutAddressAsync(request);
    }

    public async Task<CheckoutResponse> UpdateCheckoutShippingAsync(string checkoutId, string shippingMethodId)
    {
        var request = new CheckoutShippingRequest
        {
            CheckoutId = checkoutId,
            ShippingMethodId = shippingMethodId
        };
        return await _checkoutService.UpdateCheckoutShippingAsync(request);
    }

    public async Task<CheckoutResponse> UpdateCheckoutPaymentAsync(string checkoutId, string paymentMethodId)
    {
        var request = new CheckoutPaymentRequest
        {
            CheckoutId = checkoutId,
            PaymentMethodId = paymentMethodId
        };
        return await _checkoutService.UpdateCheckoutPaymentAsync(request);
    }

    public async Task<CouponResponse> ApplyCouponAsync(string checkoutId, string couponCode)
    {
        var request = new CouponRequest
        {
            CheckoutId = checkoutId,
            CouponCode = couponCode
        };
        return await _checkoutService.ApplyCouponAsync(request);
    }

    public async Task<CheckoutResponse> RemoveCouponAsync(string checkoutId)
    {
        var request = new RemoveCouponRequest
        {
            CheckoutId = checkoutId
        };
        return await _checkoutService.RemoveCouponAsync(request);
    }

    public async Task<OrderTotalResponse> CalculateOrderTotalAsync(string checkoutId)
    {
        var request = new CalculateTotalRequest
        {
            CheckoutId = checkoutId
        };
        return await _checkoutService.CalculateOrderTotalAsync(request);
    }

    public async Task<OrderConfirmationResponse> CompleteCheckoutAsync(string checkoutId)
    {
        var request = new CompleteCheckoutRequest
        {
            CheckoutId = checkoutId
        };
        return await _checkoutService.CompleteCheckoutAsync(request);
    }

    // OrderShippingService methods
    public async Task<ShippingMethodsResponse> GetShippingMethodsAsync()
    {
        var request = new ShippingMethodsRequest();
        return await _shippingService.GetShippingMethodsAsync(request);
    }

    public async Task<CalculateShippingResponse> CalculateShippingAsync(string shippingMethodId, AddressDto origin, AddressDto destination, List<CartItemDto> items)
    {
        var request = new CalculateShippingRequest
        {
            ShippingMethodId = shippingMethodId,
            Origin = origin,
            Destination = destination
        };
        request.Items.AddRange(items);
        return await _shippingService.CalculateShippingAsync(request);
    }

    // PaymentService methods
    public async Task<PaymentMethodsResponse> GetPaymentMethodsAsync()
    {
        var request = new PaymentMethodsRequest();
        return await _paymentService.GetPaymentMethodsAsync(request);
    }

    public async Task<InitiatePaymentResponse> InitiatePaymentAsync(long orderId, string paymentMethodId, double amount, string currency = "USD")
    {
        var request = new InitiatePaymentRequest
        {
            OrderId = orderId,
            PaymentMethodId = paymentMethodId,
            Amount = amount,
            Currency = currency
        };
        return await _paymentService.InitiatePaymentAsync(request);
    }

    public async Task<ProcessPaymentResponse> ProcessPaymentAsync(string paymentIntentId, string gatewayResponse = null)
    {
        var request = new ProcessPaymentRequest
        {
            PaymentIntentId = paymentIntentId,
            GatewayResponse = gatewayResponse ?? string.Empty
        };
        return await _paymentService.ProcessPaymentAsync(request);
    }

    public async Task<VerifyPaymentResponse> VerifyPaymentAsync(string transactionId)
    {
        var request = new VerifyPaymentRequest
        {
            TransactionId = transactionId
        };
        return await _paymentService.VerifyPaymentAsync(request);
    }
} 