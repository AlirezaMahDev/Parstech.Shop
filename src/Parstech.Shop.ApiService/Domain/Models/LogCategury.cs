using System;
using System.Collections.Generic;

namespace Shop.Domain.Models;

public partial class LogCategury
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();
}
