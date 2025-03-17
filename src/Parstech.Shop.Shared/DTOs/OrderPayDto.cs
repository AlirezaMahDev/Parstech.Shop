namespace Parstech.Shop.Shared.DTOs;

public class OrderPayDto
{
    public int Id { get; set; }

    public string? DepositCode { get; set; }

    public string? Description { get; set; }

    public int PayTypeId { get; set; }

    public int OrderId { get; set; }

    public int PayStatusTypeId { get; set; }
    public string TypeName { get; set; }
    public string PayTracking { get; set; } //کد پیگیری تراکنش
    public long Price { get; set; }
}