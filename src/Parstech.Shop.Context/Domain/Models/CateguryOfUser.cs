namespace Parstech.Shop.Context.Domain.Models;

public partial class CateguryOfUser
{
    public int Id { get; set; }

    public string CateguryName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<UserCategury> UserCateguries { get; set; } = new List<UserCategury>();
}
