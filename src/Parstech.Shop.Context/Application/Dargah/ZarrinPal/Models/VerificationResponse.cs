namespace Parstech.Shop.Context.Application.Dargah.ZarrinPal.Models;

public class VerificationResponse
{

    public bool IsSuccess { get { return Status == 100; } set { this.IsSuccess = value; } }
    public String RefID { get; set; }
    public int Status { get; set; }
    public ExtraDetail ExtraDetail { get; set; }
 

}

public class ExtraDetail
{
    public Transaction Transaction;
}


public class Transaction
{
    public String CardPanHash;
    public String CardPanMask;
}