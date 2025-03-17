namespace Parstech.Shop.Shared.Models;

public partial class ProductLogType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ProductLog> ProductLogs { get; set; } = new List<ProductLog>();
}