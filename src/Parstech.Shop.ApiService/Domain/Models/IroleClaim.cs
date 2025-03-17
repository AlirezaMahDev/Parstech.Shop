namespace Parstech.Shop.ApiService.Domain.Models;

public partial class IroleClaim
{
    public int Id { get; set; }

    public string RoleId { get; set; } = null!;

    public string? ClaimType { get; set; }

    public string? ClaimValue { get; set; }

    public virtual Irole Role { get; set; } = null!;
}