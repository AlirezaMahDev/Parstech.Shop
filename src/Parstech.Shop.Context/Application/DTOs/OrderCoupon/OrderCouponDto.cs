namespace Parstech.Shop.Context.Application.DTOs.OrderCoupon;

public class OrderCouponDto
{
    public int Id { get; set; }

    public int OrderId { get; set; }
    public string OrderCode { get; set; }

    public int CouponId { get; set; }
    public string CouponCode { get; set; }
    public string CouponType { get; set; }

    public long DiscountPrice { get; set; }
}