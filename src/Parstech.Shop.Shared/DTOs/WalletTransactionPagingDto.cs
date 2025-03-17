namespace Parstech.Shop.Shared.DTOs;

public class WalletTransactionPagingDto
{
    public int Take { get; set; }
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
    public Array List { get; set; }
    public List<WalletTransactionReportDto> walletTransactions { get; set; }
}