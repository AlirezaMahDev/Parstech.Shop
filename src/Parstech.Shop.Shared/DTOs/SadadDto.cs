namespace Parstech.Shop.ApiService.Application.DTOs;

public class RequestPaymentResult
{
    public int ResCode { get; set; }
    public string Description { get; set; }
    public string Token { get; set; }
}

public class CallbackRequestPayment
{
    public string PrimaryAccNo { get; set; }
    public string HashedCardNo { get; set; }
    public int OrderId { get; set; }

    public string SwitchResCode { get; set; }
    public string ResCode { get; set; }
    public string Token { get; set; }
}

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

public class VerifyResultData
{
    public int ResCode { get; set; }
    public string Description { get; set; }
    public string Amount { get; set; }
    public string RetrivalRefNo { get; set; }
    public string SystemTraceNo { get; set; }
    public string OrderId { get; set; }
}