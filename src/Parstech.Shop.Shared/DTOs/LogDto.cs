namespace Parstech.Shop.Shared.DTOs;

public class LogDto
{
    public List<ProductLogDto> SaleLogDtos { get; set; }
    public List<ProductLogDto> BaseLogDtos { get; set; }
    public List<ProductLogDto> PriceLogDtos { get; set; }
    public List<ProductLogDto> DiscountLogDtos { get; set; }
    public List<ProductRepresentationDto> ProductRepresentationDtos { get; set; }
}