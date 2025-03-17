namespace Parstech.Shop.ApiService.Domain.Models;

public partial class ProductLog
{
    public int Id { get; set; }

    public int ProductLogTypeId { get; set; }

    public int ProductStockPriceId { get; set; }

    public int UserId { get; set; }

    public DateTime CreateDate { get; set; }

    public string OldValue { get; set; } = null!;

    public string NewValue { get; set; } = null!;

    public virtual ProductLogType ProductLogType { get; set; } = null!;

    public virtual ProductStockPrice ProductStockPrice { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}