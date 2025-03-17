namespace Parstech.Shop.Shared.DTOs;

public class TransactionParameterDto
{
    public int CurrentPage { get; set; }
    public int TakePage { get; set; }
    public int PageCount { get; set; }
    public string UserFilter { get; set; }
    public string FromDate { get; set; }
    public string ToDate { get; set; }
    public string WalletType { get; set; }
    public int TransactionType { get; set; }
}