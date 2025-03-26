namespace Parstech.Shop.Context.Domain.Models;

public partial class SectionDetail
{
    public int Id { get; set; }

    public int SectionId { get; set; }

    public string? Image { get; set; }

    public string? Alt { get; set; }

    public string? Link { get; set; }

    public string? Caption { get; set; }

    public string? SubCaption { get; set; }

    public int SectionTypeId { get; set; }

    public string? BackgroundImage { get; set; }

    public string? BackgroundColor { get; set; }

    public string? SlideNavName { get; set; }

    public int? ProductId { get; set; }

    public string? ResponsiveSize { get; set; }

    public int? ColSpace { get; set; }

    public virtual Section Section { get; set; } = null!;

    public virtual SectionType SectionType { get; set; } = null!;
}
