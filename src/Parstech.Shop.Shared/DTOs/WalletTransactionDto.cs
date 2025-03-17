namespace Parstech.Shop.Shared.DTOs;

public class WalletTransactionDto
{
    public int Id { get; set; }

    public int WalletId { get; set; }

    public int Price { get; set; }
    public string InputPrice { get; set; }

    public string Type { get; set; } = null!;

    public int TypeId { get; set; }

    public string TypeName { get; set; }

    public string? TrackingCode { get; set; }

    public string? Description { get; set; }

    public int? Persent { get; set; }

    public int? Month { get; set; }

    public bool? Start { get; set; }
    public bool? Active { get; set; }
    public string StartName { get; set; }

    public DateTime CreateDate { get; set; }
    public string CreateDateShamsi { get; set; }

    public DateTime? ExpireDate { get; set; }
    public string ExpireDateShamsi { get; set; }

    public string? FileName { get; set; }
}