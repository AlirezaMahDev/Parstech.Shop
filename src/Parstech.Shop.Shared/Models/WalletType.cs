namespace Parstech.Shop.Shared.Models;

public partial class WalletType
{
    public int TypeId { get; set; }

    public string TypeTitle { get; set; } = null!;

    public string? Color { get; set; }

    public virtual ICollection<WalletTransaction> WalletTransactions { get; set; } = new List<WalletTransaction>();
}