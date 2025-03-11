using System;
using System.Collections.Generic;

namespace Shop.Domain.Models;

public partial class CouponType
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();
}
