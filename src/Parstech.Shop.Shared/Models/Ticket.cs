namespace Parstech.Shop.Shared.Models;

public partial class Ticket
{
    public int TicketId { get; set; }

    public int StatusId { get; set; }

    public int UserId { get; set; }

    public int DepartmentId { get; set; }

    public string TicketCaption { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public virtual TicketStatus Status { get; set; } = null!;

    public virtual ICollection<TicketDetail> TicketDetails { get; set; } = new List<TicketDetail>();

    public virtual User User { get; set; } = null!;
}