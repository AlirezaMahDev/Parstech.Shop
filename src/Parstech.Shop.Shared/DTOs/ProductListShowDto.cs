namespace Parstech.Shop.Shared.DTOs;

public class ProductListShowDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int ProductStockPriceId { get; set; }
    public string Name { get; set; }
    public string? LatinName { get; set; }
    public string Image { get; set; }
    public string CateguryName { get; set; }
    public string CateguryLatinName { get; set; }
    public int Quantity { get; set; }
    public long SalePrice { get; set; }
    public long DiscountPrice { get; set; }
    public DateTime? DiscountDate { get; set; }
    public string? ShortDescription { get; set; }
    public string? ShortLink { get; set; }
    public string? VariationName { get; set; }
    public int SectionId { get; set; }
    public string SectionName { get; set; }
    public bool? ShowInDiscountPanels { get; set; }
}