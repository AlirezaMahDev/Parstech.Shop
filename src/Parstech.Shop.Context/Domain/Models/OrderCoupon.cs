namespace Parstech.Shop.Context.Domain.Models;

public partial class OrderCoupon
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int CouponId { get; set; }

    public long DiscountPrice { get; set; }

    public virtual Coupon Coupon { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
