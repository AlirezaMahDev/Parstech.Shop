namespace Parstech.Shop.ApiService.Application.Dargah.ZarrinPal;

internal class URLs
{
    public bool IsSandBox { set; private get; }
    private bool IsWithExtra;


    private const string PAYMENT_REQ_URL = "https://{0}zarinpal.com/pg/rest/WebGate/PaymentRequest{1}.json";
    private const string PAYMENT_PG_URL = "https://{0}zarinpal.com/pg/StartPay/{1}/ZarinGate";

    private const string PAYMENT_VERIFICATION_URL =
        "https://{0}zarinpal.com/pg/rest/WebGate/PaymentVerification{1}.json";

    private const string SAND_BOX = "sandbox.";
    private const string WWW = "www.";
    private const string WITH_EXTRA = "WithExtra";


    public URLs(bool IsSandBox, bool IsWithExtra)
    {
        this.IsSandBox = IsSandBox;
        this.IsWithExtra = IsWithExtra;
    }

    public URLs(bool IsSandBox)
    {
        this.IsSandBox = IsSandBox;
    }


    public string GetPaymentRequestURL()
    {
        return string.Format(PAYMENT_REQ_URL, IsSandBox ? SAND_BOX : WWW, IsWithExtra ? WITH_EXTRA : "");
    }

    public string GetPaymenGatewayURL(string Authority)
    {
        return string.Format(PAYMENT_PG_URL, IsSandBox ? SAND_BOX : WWW, Authority);
    }

    public string GetVerificationURL()
    {
        return string.Format(PAYMENT_VERIFICATION_URL, IsSandBox ? SAND_BOX : WWW, IsWithExtra ? WITH_EXTRA : "");
    }
}