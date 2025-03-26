namespace Parstech.Shop.Context.Domain.Models;

public partial class UserCategury
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int CateguryId { get; set; }

    public virtual CateguryOfUser Categury { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
