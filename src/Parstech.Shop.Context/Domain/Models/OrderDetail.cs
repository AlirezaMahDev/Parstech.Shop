namespace Parstech.Shop.Context.Domain.Models;

public partial class OrderDetail
{
    public int Id { get; set; }

    public int Count { get; set; }

    public long Price { get; set; }

    public long Tax { get; set; }

    public long DetailSum { get; set; }

    public long Discount { get; set; }

    public long Total { get; set; }

    public int ProductStockPriceId { get; set; }

    public int OrderId { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual ProductStockPrice ProductStockPrice { get; set; } = null!;
}
