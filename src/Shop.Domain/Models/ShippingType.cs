﻿using System;
using System.Collections.Generic;

namespace Shop.Domain.Models;

public partial class ShippingType
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public long Price { get; set; }

    public virtual ICollection<OrderShipping> OrderShippings { get; set; } = new List<OrderShipping>();
}
