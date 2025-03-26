namespace Parstech.Shop.Context.Domain.Models;

public partial class ProductRelated
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int FkProductId { get; set; }

    public virtual Product FkProduct { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
