namespace Parstech.Shop.ApiService.Application.DTOs;

public class CouponPcuDto
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public bool YesOrNo { get; set; }

    public int FkId { get; set; }

    public int CouponId { get; set; }
}

public class CouponListPcuDto
{
    public string Type { get; set; }
    public List<CouponCheckPcuDto> list { get; set; }
}

public class CouponCheckPcuDto
{
    public int FkId { get; set; }

    public int CouponId { get; set; }
}