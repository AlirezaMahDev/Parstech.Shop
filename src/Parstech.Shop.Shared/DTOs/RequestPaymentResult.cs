namespace Parstech.Shop.Shared.DTOs;

public class RequestPaymentResult
{
    public int ResCode { get; set; }
    public string Description { get; set; }
    public string Token { get; set; }
}