namespace Parstech.Shop.Context.Application.DTOs.Order;

public class NoPayResponseGenKey
{
    public int ResponseCode { get; set; }
    public string BnplKey { get; set; }
    public string Message { get; set; }
    public int Amount { get; set; }
    public string FormatedAmount { get; set; }
}