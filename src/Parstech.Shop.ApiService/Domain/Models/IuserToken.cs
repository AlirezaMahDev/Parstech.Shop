using System;
using System.Collections.Generic;

namespace Shop.Domain.Models;

public partial class IuserToken
{
    public string UserId { get; set; } = null!;

    public string LoginProvider { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Value { get; set; }

    public virtual Iuser User { get; set; } = null!;
}
