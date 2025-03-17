namespace Parstech.Shop.Shared.DTOs;

public class CouponPcuDto
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public bool YesOrNo { get; set; }

    public int FkId { get; set; }

    public int CouponId { get; set; }
}