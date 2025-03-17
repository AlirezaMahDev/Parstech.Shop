namespace Parstech.Shop.ApiService.Domain.Models;

public partial class SectionType
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<SectionDetail> SectionDetails { get; set; } = new List<SectionDetail>();

    public virtual ICollection<Section> Sections { get; set; } = new List<Section>();
}