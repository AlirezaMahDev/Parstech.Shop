namespace Parstech.Shop.Context.Application.DTOs.OrderPay;

public class OrderPayDto
{
    public int Id { get; set; }

    public string? DepositCode { get; set; }

    public string? Description { get; set; }

    public int PayTypeId { get; set; }

    public int OrderId { get; set; }

    public int PayStatusTypeId { get; set; }
    public string TypeName { get; set; }
    public string PayTracking { get; set; }//کد پیگیری تراکنش
    public long Price { get; set; }
}

public class ResponseOrderPayDto
{
    public bool IsSuccessed { get; set; }
    public Object Object { get; set; } = null!;
    public List<Domain.Models.OrderPay> orderPayResult { get; set; }
    public string? Message { get; set; }

}