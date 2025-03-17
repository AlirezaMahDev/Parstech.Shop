namespace Parstech.Shop.Shared.DTOs;

public class ApiBuyClass
{
    public bool haveCash { get; set; } = false;
    public bool haveCredit { get; set; } = true;
    public bool haveCheck { get; set; } = false;
    public bool haveBon { get; set; } = false;
    public bool taavoniDebt { get; set; } = false;
    public int cashTransactionValue { get; set; } = 0;
    public int creditTransactionValue { get; set; }
    public int checkTransactionValue { get; set; } = 0;
    public int bonTransactionValue { get; set; } = 0;
    public string nationalCode { get; set; }
    public int sellerId { get; set; } = 23;
    public int phoneNumberId { get; set; }
    public object checksJsonData { get; set; } = null;
    public object bonsJsonData { get; set; } = null;
    public string personelCode { get; set; } = "";
    public string explains { get; set; } = "";
}