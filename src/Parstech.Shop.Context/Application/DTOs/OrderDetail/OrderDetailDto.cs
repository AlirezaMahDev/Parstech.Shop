namespace Parstech.Shop.Context.Application.DTOs.OrderDetail;

public class OrderDetailDto
{
    public int Id { get; set; }

    public int Count { get; set; }

    public long Price { get; set; }
    public long DetailSum { get; set; }

    public long Discount { get; set; }

    public long Tax { get; set; }

    public long Total { get; set; }

    public int ProductId { get; set; }
    public int ProductStockPriceId { get; set; }
    public string ProductName { get; set; }
    public string ProductCode { get; set; }
    public string Image { get; set; }
    public string Alt { get; set; }

    public int OrderId { get; set; }
    public string StoreName { get; set; }

}