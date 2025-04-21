using System;
using System.Collections.Generic;

namespace Shop.Domain.Models;

public partial class IuserClaim
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public string? ClaimType { get; set; }

    public string? ClaimValue { get; set; }

    public virtual Iuser User { get; set; } = null!;
}
