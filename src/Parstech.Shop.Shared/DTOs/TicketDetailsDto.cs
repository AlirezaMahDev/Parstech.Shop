namespace Parstech.Shop.Shared.DTOs;

public class TicketDetailsDto
{
    public int DetailId { get; set; }

    public int TicketId { get; set; }

    public int TypeId { get; set; }
    public string TypeTitle { get; set; }

    public string TicketText { get; set; } = null!;

    public DateTime Time { get; set; }

    public string? TicketFile { get; set; }
}