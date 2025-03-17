namespace Parstech.Shop.ApiService.Domain.Models;

public partial class ProductType
{
    public int Id { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}