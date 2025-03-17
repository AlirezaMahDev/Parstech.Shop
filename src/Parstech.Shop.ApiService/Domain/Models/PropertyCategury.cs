namespace Parstech.Shop.ApiService.Domain.Models;

public partial class PropertyCategury
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Property> Properties { get; set; } = new List<Property>();
}