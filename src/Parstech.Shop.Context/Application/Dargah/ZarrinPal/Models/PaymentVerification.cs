namespace Parstech.Shop.Context.Application.Dargah.ZarrinPal.Models;

public class PaymentVerification
{
    public long Amount { private set; get; }
    public String MerchantID { private set; get; }
    public String Authority { private set; get; }


    public PaymentVerification(String MerchantID, long Amount, String Authority)
    {
        this.Amount = Amount;
        this.MerchantID = MerchantID;
        this.Authority = Authority;
    }
}