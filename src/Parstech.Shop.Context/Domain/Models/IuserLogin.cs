namespace Parstech.Shop.Context.Domain.Models;

public partial class IuserLogin
{
    public string LoginProvider { get; set; } = null!;

    public string ProviderKey { get; set; } = null!;

    public string? ProviderDisplayName { get; set; }

    public string UserId { get; set; } = null!;

    public virtual Iuser User { get; set; } = null!;
}
