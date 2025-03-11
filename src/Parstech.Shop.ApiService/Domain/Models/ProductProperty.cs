using System;
using System.Collections.Generic;

namespace Shop.Domain.Models;

public partial class ProductProperty
{
    public int Id { get; set; }

    public int PropertyId { get; set; }

    public int ProductId { get; set; }

    public string Value { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual Property Property { get; set; } = null!;
}
