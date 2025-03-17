namespace Parstech.Shop.ApiService.Application.DTOs;

public class RahkaranOrderDto
{
    public int OrderId { get; set; }
    public string OrderCode { get; set; }

    public string? RahkaranPishNumber { get; set; }

    public string? RahakaranFactorNumber { get; set; }

    public string? RahakaranFactorSerial { get; set; }
}