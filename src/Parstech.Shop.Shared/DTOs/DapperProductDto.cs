namespace Parstech.Shop.Shared.DTOs;

public class DapperProductDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int ProductStockPriceiId { get; set; }
    public string Name { get; set; } = null!;
    public string? LatinName { get; set; }

    public string? Code { get; set; }


    public long SalePrice { get; set; }

    public long DiscountPrice { get; set; }


    public bool StockStatus { get; set; }

    public int Quantity { get; set; }
    public int MaximumSaleInOrder { get; set; }


    public string? ShortDescription { get; set; }

    public string? ShortLink { get; set; }

    public int TypeId { get; set; }

    public string StoreName { get; set; }
    public string GroupTitle { get; set; }

    public int TaxId { get; set; }
    public int RepId { get; set; }

    public int StoreId { get; set; }
    public int? ParentId { get; set; }

    public string LatinStoreName { get; set; }


    public DateTime CreateDate { get; set; }

    public int BrandId { get; set; }
    public string BrandTitle { get; set; }
    public string LatinBrandTitle { get; set; }
    public bool SingleSale { get; set; }
    public int? QuantityPerBundle { get; set; }
    public string Description { get; set; }

    public string? TaxCode { get; set; }
    public string? Keywords { get; set; }
    public DateTime? DiscountDate { get; set; }
    public bool IsActive { get; set; }
    public int SectionId { get; set; }
    public bool? ShowInDiscountPanels { get; set; }
    public int? BestStockId { get; set; }

    public int? BestStockUserCateguryId { get; set; }
    public int? CateguryOfUserId { get; set; }
    public string CateguryOfUserType { get; set; }
}