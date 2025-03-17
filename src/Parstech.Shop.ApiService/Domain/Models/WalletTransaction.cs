namespace Parstech.Shop.ApiService.Domain.Models;

public partial class WalletTransaction
{
    public int Id { get; set; }

    public int WalletId { get; set; }

    public int Price { get; set; }

    public string Type { get; set; } = null!;

    public int TypeId { get; set; }

    public string? TrackingCode { get; set; }

    public string? Description { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? ExpireDate { get; set; }

    public string? FileName { get; set; }

    public int? Persent { get; set; }

    public int? Month { get; set; }

    public bool? Start { get; set; }

    public bool? Active { get; set; }

    public int? ParentFecilitiesId { get; set; }

    public virtual WalletType TypeNavigation { get; set; } = null!;

    public virtual Wallet Wallet { get; set; } = null!;
}