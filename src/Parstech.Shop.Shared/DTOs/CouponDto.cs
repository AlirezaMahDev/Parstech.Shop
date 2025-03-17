namespace Parstech.Shop.Shared.DTOs;

public class CouponDto
{
    public int Id { get; set; }

    public int CouponTypeId { get; set; }
    public string CouponTypeName { get; set; }

    public string Code { get; set; } = null!;

    public string ExpireDate { get; set; }
    public string ExpireDateShamsi { get; set; }

    public bool JustNewUser { get; set; }

    public long? MinPrice { get; set; }

    public long? MaxPrice { get; set; }

    public bool TwoUseSameTime { get; set; }

    public int? LimitUse { get; set; }

    public int? LimitEachUser { get; set; }

    public string Categury { get; set; } = null!;

    public string Products { get; set; } = null!;

    public string Users { get; set; } = null!;

    public int Persent { get; set; }

    public long Amount { get; set; }
}