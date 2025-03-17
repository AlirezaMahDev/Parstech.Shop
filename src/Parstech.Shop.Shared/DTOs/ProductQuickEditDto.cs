namespace Parstech.Shop.Shared.DTOs;

public class ProductQuickEditDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? LatinName { get; set; }
    public int TypeId { get; set; }
    public string? VariationName { get; set; }
    public int StoreId { get; set; }
    public int? ParentId { get; set; }
    public int BrandId { get; set; }
    public int TaxId { get; set; }
    public int Score { get; set; }
    public int? QuantityPerBundle { get; set; }
}