using System;
using System.Collections.Generic;

namespace Shop.Domain.Models;

public partial class TicketType
{
    public int TypeId { get; set; }

    public string TypeTitle { get; set; } = null!;

    public virtual ICollection<TicketDetail> TicketDetails { get; set; } = new List<TicketDetail>();
}
