namespace Parstech.Shop.Context.Domain.Models;

public partial class RepresentationType
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public string Color { get; set; } = null!;

    public virtual ICollection<ProductRepresentation> ProductRepresentations { get; set; } = new List<ProductRepresentation>();
}
