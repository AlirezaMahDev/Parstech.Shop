namespace Parstech.Shop.Shared.DTOs;

public class RahkaranOrderDto
{
    public int OrderId { get; set; }
    public string OrderCode { get; set; }

    public string? RahkaranPishNumber { get; set; }

    public string? RahakaranFactorNumber { get; set; }

    public string? RahakaranFactorSerial { get; set; }
}