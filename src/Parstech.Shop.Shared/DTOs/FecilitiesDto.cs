namespace Parstech.Shop.ApiService.Application.DTOs;

public class FecilitiesDto
{
    public int WalletId { get; set; }
    public double Price { get; set; }
    public DateTime StartTime { get; set; }
    public int Sud { get; set; }
    public int GhestCount { get; set; }
    public double Ghest { get; set; }
    public double TotalPrice { get; set; }
    public int KarmozdPersent { get; set; }
    public double Karmozd { get; set; }
    public string Serial { get; set; }
    public string Type { get; set; }
    public string OrderCode { get; set; }
    public int? ParentFecilitiesId { get; set; }
}