namespace Parstech.Shop.ApiService.Domain.Models;

public partial class OrderPay
{
    public int Id { get; set; }

    public string? DepositCode { get; set; }

    public string? Description { get; set; }

    public int PayTypeId { get; set; }

    public int OrderId { get; set; }

    public int PayStatusTypeId { get; set; }

    public long Price { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual PayStatusType PayStatusType { get; set; } = null!;

    public virtual PayType PayType { get; set; } = null!;
}