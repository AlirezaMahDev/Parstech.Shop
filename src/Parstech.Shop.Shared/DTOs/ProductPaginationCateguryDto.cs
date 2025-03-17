namespace Parstech.Shop.Shared.DTOs;

public class ProductPaginationCateguryDto
{
    public int ProductStockPriceId { get; set; }
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public long SalePrice { get; set; }

    public long DiscountPrice { get; set; }

    public int Quantity { get; set; }
    public int TypeId { get; set; }
    public string Image { get; set; }
    public int StoreId { get; set; }
    public string StoreName { get; set; }
    public string ShortLink { get; set; }
    public long FinalPrice { get; set; }
    public long FinalDiscountPrice { get; set; }
    public long FinalQuantity { get; set; }
}