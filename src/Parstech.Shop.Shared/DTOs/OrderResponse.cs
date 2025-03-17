namespace Parstech.Shop.Shared.DTOs;

public class OrderResponse
{
    public bool Status { get; set; }
    public string Message { get; set; }
    public long Discount { get; set; }
}