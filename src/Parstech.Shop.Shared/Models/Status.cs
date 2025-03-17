namespace Parstech.Shop.Shared.Models;

public partial class Status
{
    public int Id { get; set; }

    public string StatusName { get; set; } = null!;

    public string StatusLatinName { get; set; } = null!;

    public string? Icon { get; set; }

    public int? Olaviyat { get; set; }

    public virtual ICollection<OrderStatus> OrderStatuses { get; set; } = new List<OrderStatus>();
}