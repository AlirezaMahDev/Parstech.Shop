using System;
using System.Collections.Generic;

namespace Shop.Domain.Models;

public partial class CreditProductStockPrice
{
    public int Id { get; set; }

    public int ProductStockPriceId { get; set; }

    public int Persent { get; set; }

    public int Month { get; set; }

    public long PrePay { get; set; }

    public long PayMonth { get; set; }

    public long Total { get; set; }

    public string? Description { get; set; }

    public bool Active { get; set; }

    public virtual ProductStockPrice ProductStockPrice { get; set; } = null!;
}
