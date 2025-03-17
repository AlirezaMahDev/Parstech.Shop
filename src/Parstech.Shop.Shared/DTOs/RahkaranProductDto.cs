namespace Parstech.Shop.ApiService.Application.DTOs;

public class RahkaranProductDto
{
    public int? StockId { get; set; }
    public int? DetailId { get; set; }
    public int? Count { get; set; }
    public long? Price { get; set; }

    public string Name { get; set; }

    public string? Code { get; set; }

    public string? VariationName { get; set; }

    public int? ProductId { get; set; }

    public string? RahkaranProductId { get; set; }
    public int? RahkaranUnitId { get; set; }
}