using System;
using System.Collections.Generic;

namespace Shop.Domain.Models;

public partial class Tax
{
    public int Id { get; set; }

    public string TaxName { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
