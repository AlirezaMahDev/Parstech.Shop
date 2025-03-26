namespace Parstech.Shop.Context.Domain.Models;

public partial class Irole
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? NormalizedName { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? PersianName { get; set; }

    public virtual ICollection<IroleClaim> IroleClaims { get; set; } = new List<IroleClaim>();

    public virtual ICollection<Iuser> Users { get; set; } = new List<Iuser>();
}
