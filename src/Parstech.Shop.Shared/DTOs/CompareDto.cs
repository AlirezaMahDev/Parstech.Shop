namespace Parstech.Shop.Shared.DTOs;

public class CompareDto
{
    public int userProductId { get; set; }
    public int productId { get; set; }
    public int productStockId { get; set; }
    public string name { get; set; }
    public string shortLink { get; set; }
    public string code { get; set; }
    public string image { get; set; }
    public List<ProductPropertyDto> commonProperties { get; set; }
    public List<ProductPropertyDto> productProperties { get; set; }
}