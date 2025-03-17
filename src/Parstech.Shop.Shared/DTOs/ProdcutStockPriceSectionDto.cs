namespace Parstech.Shop.Shared.DTOs;

public class ProdcutStockPriceSectionDto
{
    public int ProdutSrockPriceId { get; set; }
    public bool? ShowInDiscountPanels { get; set; }
    public List<SectionDto> sections { get; set; }
}