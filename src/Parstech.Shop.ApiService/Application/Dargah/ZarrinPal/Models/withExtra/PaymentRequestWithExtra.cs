namespace Parstech.Shop.ApiService.Application.Dargah.ZarrinPal.Models.withExtra;

public class PaymentRequestWithExtra : PaymentRequest
{
    public object AdditionalData { get; set; }

    public PaymentRequestWithExtra(string MerchantID,
        long Amount,
        string CallbackURL,
        string Description,
        object AdditionalData) :
        base(MerchantID, Amount, CallbackURL, Description)
    {
        this.AdditionalData = AdditionalData;
    }
}