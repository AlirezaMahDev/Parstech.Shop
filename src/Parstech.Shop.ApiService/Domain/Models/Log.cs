using System;
using System.Collections.Generic;

namespace Shop.Domain.Models;

public partial class Log
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public int CreateDate { get; set; }

    public int LogCateguryId { get; set; }

    public int UserId { get; set; }

    public virtual LogCategury LogCategury { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
