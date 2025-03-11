using System;
using System.Collections.Generic;

namespace Shop.Domain.Models;

public partial class UserCategury
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int CateguryId { get; set; }

    public virtual CateguryOfUser Categury { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
