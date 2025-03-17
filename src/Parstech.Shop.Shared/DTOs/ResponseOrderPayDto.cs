using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.Shared.DTOs;

public class ResponseOrderPayDto
{
    public bool IsSuccessed { get; set; }
    public object Object { get; set; } = null!;
    public List<OrderPay> orderPayResult { get; set; }
    public string? Message { get; set; }
}