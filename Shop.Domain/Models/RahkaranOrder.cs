using System;
using System.Collections.Generic;

namespace Shop.Domain.Models;

public partial class RahkaranOrder
{
    public int? OrderId { get; set; }

    public string? RahkaranPishNumber { get; set; }

    public string? RahakaranFactorNumber { get; set; }

    public string? RahakaranFactorSerial { get; set; }
}
