namespace Parstech.Shop.Shared.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public int ProductStockPriceId { get; set; }
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;
    public string? LatinName { get; set; }

    public string? Code { get; set; }

    public long Price { get; set; }

    public long SalePrice { get; set; }

    public long DiscountPrice { get; set; }
    public DateTime? DiscountDate { get; set; }

    public long BasePrice { get; set; }

    public bool StockStatus { get; set; }

    public int Quantity { get; set; }
    public int MaximumSaleInOrder { get; set; }

    public int Score { get; set; }

    public string? Description { get; set; }

    public string? ShortDescription { get; set; }

    public string? ShortLink { get; set; }

    public int TypeId { get; set; }
    public string TypeName { get; set; }
    public string? VariationName { get; set; }

    public int StoreId { get; set; }
    public string StoreName { get; set; }
    public string LatinStoreName { get; set; }

    public string Image { get; set; }

    public int? ParentId { get; set; }
    public string ParentProductName { get; set; }

    public int BrandId { get; set; }
    public string BrandName { get; set; }
    public string LatinBrandName { get; set; }

    public int TaxId { get; set; }
    public int RepId { get; set; }
    public string RepName { get; set; }
    public DateTime CreateDate { get; set; }
    public string CreateDateShamsi { get; set; }

    public int Visit { get; set; }
    public int CateguryId { get; set; }
    public string CateguryName { get; set; }
    public string CateguryLatinName { get; set; }
    public int CountSale { get; set; }
    public bool SingleSale { get; set; }

    public int? QuantityPerBundle { get; set; }

    public string? TaxCode { get; set; }
    public string? Keywords { get; set; }

    public bool IsActive { get; set; }
    public bool? ShowInDiscountPanels { get; set; }
    public int? CateguryOfUserId { get; set; }
    public string CateguryOfUserName { get; set; }
    public string CateguryOfUserType { get; set; }
    public int? BestStockId { get; set; }
    public int? BestStockUserCateguryId { get; set; }
}