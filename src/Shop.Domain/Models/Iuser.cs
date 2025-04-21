using System;
using System.Collections.Generic;

namespace Shop.Domain.Models;

public partial class Iuser
{
    public string Id { get; set; } = null!;

    public string? UserName { get; set; }

    public string? NormalizedUserName { get; set; }

    public string? Email { get; set; }

    public string? NormalizedEmail { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    public virtual ICollection<IuserClaim> IuserClaims { get; set; } = new List<IuserClaim>();

    public virtual ICollection<IuserLogin> IuserLogins { get; set; } = new List<IuserLogin>();

    public virtual ICollection<IuserToken> IuserTokens { get; set; } = new List<IuserToken>();

    public virtual ICollection<Irole> Roles { get; set; } = new List<Irole>();
}
