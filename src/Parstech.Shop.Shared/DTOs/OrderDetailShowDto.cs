namespace Parstech.Shop.Shared.DTOs;

public class OrderDetailShowDto
{
    public List<OrderDetailDto> OrderDetailDto { get; set; }
    public OrderDto Order { get; set; }
    public UserBillingDto Costumer { get; set; }
    public OrderShippingDto OrderShipping { get; set; }
    public List<UserShippingDto> UserShippingList { get; set; }
    public int OrderShippingId { get; set; }
    public OrderCouponDto OrderCoupon { get; set; }
    public List<PayTypeDto> PayTypes { get; set; }
    public List<OrderPayDto> OrderPay { get; set; }
}