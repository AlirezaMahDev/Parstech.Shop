namespace Parstech.Shop.Shared.DTOs;

public class StatusOfOrderDto
{
    public int OrderId { get; set; }
    public int StatusId { get; set; }
    public DateTime CreateDate { get; set; }
    public string? FileName { get; set; }
    public string CreateDateShamsi { get; set; }
    public string StatusName { get; set; }
    public string CreateBy { get; set; } = null!;
}