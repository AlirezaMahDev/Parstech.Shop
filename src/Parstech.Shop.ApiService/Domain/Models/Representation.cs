namespace Parstech.Shop.ApiService.Domain.Models;

public partial class Representation
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? StateId { get; set; }

    public virtual ICollection<ProductRepresentation> ProductRepresentations { get; set; } =
        new List<ProductRepresentation>();

    public virtual ICollection<ProductStockPrice> ProductStockPrices { get; set; } = new List<ProductStockPrice>();

    public virtual State? State { get; set; }

    public virtual ICollection<UserStore> UserStores { get; set; } = new List<UserStore>();
}