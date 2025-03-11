using System;
using System.Collections.Generic;

namespace Shop.Domain.Models;

public partial class PayType
{
    public int Id { get; set; }

    public string TypeName { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<OrderPay> OrderPays { get; set; } = new List<OrderPay>();
}
