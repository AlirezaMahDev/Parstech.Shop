namespace Parstech.Shop.ApiService.Application.Dargah.ZarrinPal.Models;

public class VerificationResponse
{
    public bool IsSuccess
    {
        get => Status == 100;
        set => IsSuccess = value;
    }

    public string RefID { get; set; }
    public int Status { get; set; }
    public ExtraDetail ExtraDetail { get; set; }
}

public class ExtraDetail
{
    public Transaction Transaction;
}

public class Transaction
{
    public string CardPanHash;
    public string CardPanMask;
}