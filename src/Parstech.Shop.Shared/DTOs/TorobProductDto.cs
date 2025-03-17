namespace Parstech.Shop.Shared.DTOs;

public class TorobProductDto
{
    public string Name { get; set; }
    public int Id { get; set; }
    public int ProductId { get; set; }
    public long SalePrice { get; set; }
    public long DiscountPrice { get; set; }
    public int Quantity { get; set; }
    public int TypeId { get; set; }
    public int? ParentId { get; set; }
    public string ShortLink { get; set; }
    public DateTime? DiscountDate { get; set; }
    public string ShortDescription { get; set; }
}