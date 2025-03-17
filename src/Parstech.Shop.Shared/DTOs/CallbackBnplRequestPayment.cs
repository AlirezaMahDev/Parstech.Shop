namespace Parstech.Shop.Shared.DTOs;

public class CallbackBnplRequestPayment
{
    public int ResCode { get; set; }
    public long Amount { get; set; }
    public string Description { get; set; }
    public string Token { get; set; }
    public string RetrivalRefNo { get; set; }
    public string SystemTraceNo { get; set; }
    public string SwitchResCode { get; set; }
    public string TransactionDate { get; set; }
    public string AdditionalData { get; set; }
    public string CardHolderFullName { get; set; }
    public int OrderId { get; set; }
}