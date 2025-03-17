namespace Parstech.Shop.ApiService.Application.Dargah.ZarrinPal.Models;

public class PaymentResponse
{
    public string Authority { set; get; }
    public int Status { set; get; }
    public string PaymentURL { set; get; }
}