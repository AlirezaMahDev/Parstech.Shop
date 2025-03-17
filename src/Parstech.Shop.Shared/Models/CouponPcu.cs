namespace Parstech.Shop.Shared.Models;

public partial class CouponPcu
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public bool YesOrNo { get; set; }

    public int FkId { get; set; }

    public int CouponId { get; set; }

    public virtual Coupon Coupon { get; set; } = null!;
}