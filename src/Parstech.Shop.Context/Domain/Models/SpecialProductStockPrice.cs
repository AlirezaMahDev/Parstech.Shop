namespace Parstech.Shop.Context.Domain.Models;

public partial class SpecialProductStockPrice
{
    public int Id { get; set; }

    public int ProductStockPriceId { get; set; }

    public int? CateguryId { get; set; }

    public string? Description { get; set; }
}
