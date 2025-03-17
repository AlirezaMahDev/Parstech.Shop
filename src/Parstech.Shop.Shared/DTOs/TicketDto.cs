namespace Parstech.Shop.Shared.DTOs;

public class TicketDto
{
    public int TicketId { get; set; }

    public int StatusId { get; set; }

    public string StatusTitle { get; set; }

    public int UserId { get; set; }

    public int DepartmentId { get; set; }

    public string TicketCaption { get; set; } = null!;

    public DateTime CreateDate { get; set; }
}