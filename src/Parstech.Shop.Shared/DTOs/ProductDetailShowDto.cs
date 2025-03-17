namespace Parstech.Shop.Shared.DTOs;

public class ProductDetailShowDto
{
    public int Id { get; set; }
    public int ProductStockPriceId { get; set; }
    public string? ShortLink { get; set; }
    public string Name { get; set; }
    public string? LatinName { get; set; }
    public long SalePrice { get; set; }
    public long DiscountPrice { get; set; }
    public int Score { get; set; }
    public string Code { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
    public string ShortDescription { get; set; }
    public string? VariationName { get; set; }
    public List<ProductDto> RelatedStores { get; set; }
    public List<BaseProductPropertyDto> Properties { get; set; }
    public List<ProductPropertyDto> SomeProperties { get; set; }
    public List<ProductDto> Accessories { get; set; }
    public List<ProductDto> Childs { get; set; }
    public BrandDto Brand { get; set; }
    public string Store { get; set; }
    public string StoreLatin { get; set; }
    public DateTime? DiscountDate { get; set; }
    public int? CateguryOfUserId { get; set; }
}