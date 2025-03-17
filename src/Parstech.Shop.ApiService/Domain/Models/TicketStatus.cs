namespace Parstech.Shop.ApiService.Domain.Models;

public partial class TicketStatus
{
    public int StatusId { get; set; }

    public string StatusTitle { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}