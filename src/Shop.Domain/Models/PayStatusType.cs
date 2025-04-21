using System;
using System.Collections.Generic;

namespace Shop.Domain.Models;

public partial class PayStatusType
{
    public int Id { get; set; }

    public string Status { get; set; } = null!;

    public string? Color { get; set; }

    public virtual ICollection<OrderPay> OrderPays { get; set; } = new List<OrderPay>();
}
