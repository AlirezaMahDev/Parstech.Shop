using System;
using System.Collections.Generic;

namespace Shop.Domain.Models;

public partial class TicketDetail
{
    public int DetailId { get; set; }

    public int TicketId { get; set; }

    public int TypeId { get; set; }

    public string TicketText { get; set; } = null!;

    public DateTime Time { get; set; }

    public string? TicketFile { get; set; }

    public virtual Ticket Ticket { get; set; } = null!;

    public virtual TicketType Type { get; set; } = null!;
}
