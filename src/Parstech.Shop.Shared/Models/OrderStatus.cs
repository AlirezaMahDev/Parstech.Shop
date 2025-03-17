namespace Parstech.Shop.Shared.Models;

public partial class OrderStatus
{
    public int Osid { get; set; }

    public int StatusId { get; set; }

    public DateTime CreateDate { get; set; }

    public string? FileName { get; set; }

    public string? Comment { get; set; }

    public string CreateBy { get; set; } = null!;

    public bool IsActive { get; set; }

    public int OrderId { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;
}