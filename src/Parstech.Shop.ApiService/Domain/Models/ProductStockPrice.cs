namespace Parstech.Shop.ApiService.Domain.Models;

public partial class ProductStockPrice
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public long Price { get; set; }

    public long SalePrice { get; set; }

    public long DiscountPrice { get; set; }

    public long BasePrice { get; set; }

    public bool StockStatus { get; set; }

    public int Quantity { get; set; }

    public int MaximumSaleInOrder { get; set; }

    public int StoreId { get; set; }

    public int RepId { get; set; }

    public int TaxId { get; set; }

    public int? QuantityPerBundle { get; set; }

    public DateTime? DiscountDate { get; set; }

    public int? CateguryOfUserId { get; set; }

    public string? CateguryOfUserType { get; set; }

    public bool? ShowInDiscountPanels { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<ProductLog> ProductLogs { get; set; } = new List<ProductLog>();

    public virtual ICollection<ProductRepresentation> ProductRepresentations { get; set; } =
        new List<ProductRepresentation>();

    public virtual ICollection<ProductStockPriceSection> ProductStockPriceSections { get; set; } =
        new List<ProductStockPriceSection>();

    public virtual Representation Rep { get; set; } = null!;

    public virtual UserStore Store { get; set; } = null!;
}