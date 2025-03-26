namespace Parstech.Shop.Context.Application.DTOs.Wallet;

public class WalletDto
{
    public int WalletId { get; set; }

    public int UserId { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }

    public long Amount { get; set; }
    public long OrgCredit { get; set; }

    public int Coin { get; set; }

    public bool IsBlock { get; set; }

    public long Fecilities { get; set; }
}