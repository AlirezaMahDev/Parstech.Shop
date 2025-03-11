using System;
using System.Collections.Generic;

namespace Shop.Domain.Models;

public partial class SecoundPayAfterDargah
{
    public int? OrderId { get; set; }

    public int? TransactionId { get; set; }

    public int? Month { get; set; }
}
