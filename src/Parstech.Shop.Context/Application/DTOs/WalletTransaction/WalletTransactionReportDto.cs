namespace Parstech.Shop.Context.Application.DTOs.WalletTransaction;

public class WalletTransactionPagingDto
{
    public int Take { get; set; }
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
    public Array List { get; set; }
    public List<WalletTransactionReportDto> walletTransactions { get; set; }
}

public class WalletTransactionReportDto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public long Amount { get; set; }
    public long Fecilities { get; set; }
    public long OrgCredit { get; set; }
    public int Price { get; set; }
    public string Type { get; set; }
    public int TypeId { get; set; }
    public string TypeTitle { get; set; }
    public string Color { get; set; }
    public string Description { get; set; }
    public DateTime CreateDate { get; set; }
    public string CreateDateShamsi { get; set; }
    public string TrackingCode { get; set; }
    public DateTime ExpireDate { get; set; }
    public string ExpireDateShamsi { get; set; }
    public bool Start { get; set; }
    public bool? Active { get; set; }
    public int Month { get; set; }
    public int Persent { get; set; }
    public int? ParentFecilitiesId { get; set; }
    public string FirstDate { get; set; }
    public string LastDate { get; set; }
        
}

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