namespace Parstech.Shop.Shared.DTOs;

public class WalletTransactionParameterDto
{
    public int CurrentPage { get; set; }
    public int TakePage { get; set; }
    public int PageCount { get; set; }
    public string Filter { get; set; }
    public int WalletId { get; set; }
    public string Type { get; set; }
}