namespace Parstech.Shop.Context.Application.DTOs.ProductStockPriceSection;

public class ProdcutStockPriceSectionDto
{
    public int ProdutSrockPriceId { get; set; }
    public bool? ShowInDiscountPanels { get; set; }
    public List<SectionDto> sections { get; set; }
}


public class SectionDto
{
    public int Id { get; set; }

    public int SectionId { get; set; } 
    public string SectionName { get; set; } 
}