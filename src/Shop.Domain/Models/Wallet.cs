using System;
using System.Collections.Generic;

namespace Shop.Domain.Models;

public partial class Wallet
{
    public int WalletId { get; set; }

    public int UserId { get; set; }

    public long Amount { get; set; }

    public long Fecilities { get; set; }

    public long OrgCredit { get; set; }

    public int Coin { get; set; }

    public bool IsBlock { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<WalletTransaction> WalletTransactions { get; set; } = new List<WalletTransaction>();
}
