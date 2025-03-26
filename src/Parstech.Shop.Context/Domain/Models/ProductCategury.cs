namespace Parstech.Shop.Context.Domain.Models;

public partial class ProductCategury
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int CateguryId { get; set; }

    public virtual Categury Categury { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
