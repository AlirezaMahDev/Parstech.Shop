namespace Parstech.Shop.Context.Application.DTOs.Api;

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
public class TorobDto
{
    public string product_id { get; set; }
    public string page_url { get; set; }
    public string price { get; set; }
    public string availability { get; set; }
    public string old_price { get; set; }
    public string Image { get; set; }
    public string Content { get; set; }
    public string Name { get; set; }
}