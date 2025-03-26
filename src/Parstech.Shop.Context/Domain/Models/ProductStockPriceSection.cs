namespace Parstech.Shop.Context.Domain.Models;

public partial class ProductStockPriceSection
{
    public int Id { get; set; }

    public int ProductStockPriceId { get; set; }

    public int SectionId { get; set; }

    public virtual ProductStockPrice ProductStockPrice { get; set; } = null!;

    public virtual Section Section { get; set; } = null!;
}
