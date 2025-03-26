using Parstech.Shop.Context.Application.DTOs.Brand;
using Parstech.Shop.Context.Application.DTOs.Product;

namespace Parstech.Shop.Context.Application.DTOs.Section;

public class SectionDto
{
    public int Id { get; set; }

    public string SectionName { get; set; } = null!;

    public int Sort { get; set; }

    public int? CateguryId { get; set; }
    public int? ProductId { get; set; }
    public int? StoreId { get; set; }

    public int SectionTypeId { get; set; }
}


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
public class SectionDetailShowDto
{
    public int Id { get; set; }

    public string? Image { get; set; }
    public string? BackgroundImage { get; set; }
    public string? BackgroundColor { get; set; }
        
    public string? Alt { get; set; }

    public string? Link { get; set; }

    public string? Caption { get; set; }

    public string? SubCaption { get; set; }
    public string? SlideNavName { get; set; }
    public string? ResponsiveSize { get; set; }
    public int? CateguryId { get; set; }
    public string? LatingCateguryName { get; set; }
    public int? ProductId { get; set; }
    public int SectionTypeId { get; set; }
    public int? ColSpace { get; set; }
}

public class SliderShowDto
{
    public List<SectionDetailShowDto> Desktop { get; set; }
    public List<SectionDetailShowDto> Mobile { get; set; }
}