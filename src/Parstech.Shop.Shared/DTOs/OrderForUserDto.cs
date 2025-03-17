namespace Parstech.Shop.Shared.DTOs;

public class OrderForUserDto
{
    public OrderDto Order { get; set; }
    public List<OrderDetailShowDto> OrderDetails { get; set; }
    public OrderCouponDto OrderCoupon { get; set; }
    public OrderPayDto OrderPay { get; set; }
    public OrderShippingDto OrderShipping { get; set; }
}