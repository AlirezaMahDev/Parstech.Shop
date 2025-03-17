namespace Parstech.Shop.Shared.DTOs;

public class SectionAndDetailsDto
{
    public int Id { get; set; }

    public string SectionName { get; set; } = null!;

    public int Sort { get; set; }

    public int CateguryId { get; set; }
    public string latinCateguryName { get; set; }
    public int ProductId { get; set; }

    public int SectionTypeId { get; set; }

    public List<SectionDetailShowDto> SectionDetails { get; set; }
    public List<ProductDto> ProductCateguries { get; set; }
    public ProductDto Product { get; set; }
    public List<BrandDto> Brands { get; set; }
    public int? ColSpace { get; set; }
}