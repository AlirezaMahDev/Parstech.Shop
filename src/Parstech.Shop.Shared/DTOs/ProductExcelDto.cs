namespace Parstech.Shop.Shared.DTOs;

public class ProductExcelDto
{
    public string Id { get; set; }
    public string Type { get; set; } = null!;
    public string Name { get; set; }
    public string Parent { get; set; }
    public string Description { get; set; }
    public string brandId { get; set; }
}