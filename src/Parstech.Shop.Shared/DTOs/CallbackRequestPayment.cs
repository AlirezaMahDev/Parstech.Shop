namespace Parstech.Shop.Shared.DTOs;

public class CallbackRequestPayment
{
    public string PrimaryAccNo { get; set; }
    public string HashedCardNo { get; set; }
    public int OrderId { get; set; }

    public string SwitchResCode { get; set; }
    public string ResCode { get; set; }
    public string Token { get; set; }
}