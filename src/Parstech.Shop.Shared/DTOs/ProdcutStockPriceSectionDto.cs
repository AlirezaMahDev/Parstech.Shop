namespace Parstech.Shop.ApiService.Application.DTOs;

public class ProdcutStockPriceSectionDto
{
    public int ProdutSrockPriceId { get; set; }
    public bool? ShowInDiscountPanels { get; set; }
    public List<ProdcutSectionDto> sections { get; set; }
}

public class ProdcutSectionDto
{
    public int Id { get; set; }

    public int SectionId { get; set; }
    public string SectionName { get; set; }
}