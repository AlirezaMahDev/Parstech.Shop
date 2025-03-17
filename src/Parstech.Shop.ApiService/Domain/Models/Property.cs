namespace Parstech.Shop.ApiService.Domain.Models;

public partial class Property
{
    public int Id { get; set; }

    public string Caption { get; set; } = null!;

    public int PropertyCateguryId { get; set; }

    public int CateguryId { get; set; }

    public virtual Categury Categury { get; set; } = null!;

    public virtual ICollection<ProductProperty> ProductProperties { get; set; } = new List<ProductProperty>();

    public virtual PropertyCategury PropertyCategury { get; set; } = null!;
}