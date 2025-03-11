using System;
using System.Collections.Generic;

namespace Shop.Domain.Models;

public partial class RahkaranProduct
{
    public int? ProductId { get; set; }

    public string? RahkaranProductId { get; set; }

    public int? RahkaranUnitId { get; set; }
}
