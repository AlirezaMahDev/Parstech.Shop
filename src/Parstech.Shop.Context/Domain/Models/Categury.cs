namespace Parstech.Shop.Context.Domain.Models;

public partial class Categury
{
    public int GroupId { get; set; }

    public string GroupTitle { get; set; } = null!;

    public bool IsDelete { get; set; }

    public int? ParentId { get; set; }

    public string LatinGroupTitle { get; set; } = null!;

    public string? ChangeByUserName { get; set; }

    public DateTime? LastChangeTime { get; set; }

    public bool IsParnet { get; set; }

    public bool Show { get; set; }

    public string? Image { get; set; }

    public string? BackImage { get; set; }

    public string? Alt { get; set; }

    public int? Row { get; set; }

    public virtual ICollection<Categury> InverseParent { get; set; } = new List<Categury>();

    public virtual Categury? Parent { get; set; }

    public virtual ICollection<ProductCategury> ProductCateguries { get; set; } = new List<ProductCategury>();

    public virtual ICollection<Property> Properties { get; set; } = new List<Property>();
}
