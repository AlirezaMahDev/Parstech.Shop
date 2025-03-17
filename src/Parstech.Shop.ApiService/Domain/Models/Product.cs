namespace Parstech.Shop.ApiService.Domain.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? LatinName { get; set; }

    public string? Code { get; set; }

    public string? TaxCode { get; set; }

    public int Score { get; set; }

    public string? Description { get; set; }

    public string? ShortDescription { get; set; }

    public string? ShortLink { get; set; }

    public int TypeId { get; set; }

    public string? VariationName { get; set; }

    public int? ParentId { get; set; }

    public int BrandId { get; set; }

    public int TaxId { get; set; }

    public DateTime CreateDate { get; set; }

    public int Visit { get; set; }

    public bool SingleSale { get; set; }

    public string? Keywords { get; set; }

    public bool IsActive { get; set; }

    public int? BestStockId { get; set; }

    public int? BestStockUserCateguryId { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual ICollection<Product> InverseParent { get; set; } = new List<Product>();

    public virtual Product? Parent { get; set; }

    public virtual ICollection<ProductCategury> ProductCateguries { get; set; } = new List<ProductCategury>();

    public virtual ICollection<ProductComment> ProductComments { get; set; } = new List<ProductComment>();

    public virtual ICollection<ProductGallery> ProductGalleries { get; set; } = new List<ProductGallery>();

    public virtual ICollection<ProductProperty> ProductProperties { get; set; } = new List<ProductProperty>();

    public virtual ICollection<ProductRating> ProductRatings { get; set; } = new List<ProductRating>();

    public virtual ICollection<ProductRelated> ProductRelatedFkProducts { get; set; } = new List<ProductRelated>();

    public virtual ICollection<ProductRelated> ProductRelatedProducts { get; set; } = new List<ProductRelated>();

    public virtual ICollection<ProductStockPrice> ProductStockPrices { get; set; } = new List<ProductStockPrice>();

    public virtual Tax Tax { get; set; } = null!;

    public virtual ProductType Type { get; set; } = null!;
}