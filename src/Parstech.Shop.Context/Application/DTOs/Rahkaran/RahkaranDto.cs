using System.Runtime.Serialization;

namespace Parstech.Shop.Context.Application.DTOs.Rahkaran;

public class RahkaranAllDto
{
    public RahkaranOrderDto order { get; set; }
    public RahkaranUserDto customer { get; set; }
    public List<RahkaranProductDto> products { get; set; }
}


public class RahkaranDto
{
    public string system { get; set; }
    public string orderCode { get; set; }
    public List<RakaranProductItem> Products { get; set; }
    public string Currency { get; set; }
    public string Customer { get; set; }
    public string Payertype { get; set; }
    public string SalesTypeID { get; set; }
    public string SalesOfficeID { get; set; }
    public string Plant { get; set; }
    public string Inventory { get; set; }
    public string SalesAreaID { get; set; }
    public string BrokerId { get; set; }
}

public class RakaranProductItem
{
    public string ProductId { get; set; }
    public string UnitId { get; set; }
    public string Fee { get; set; }
    public string Quantity { get; set; }

}

public class RahkaranResult
{
    public List<rMessage> Messages { get; set; }
    public string Text { get; set; }
    public string QuotationId { get; set; }

}
public class rMessage
{
    public string Text { get; set; }
}

public class QuotationFollowUpRequest
{
    // شناسه پیش فاکتور
    [DataMember]
    public long IDQ { get; set; }
}
public class QuotationFollowUpResult
{
    // شناسه فاکتور
    [DataMember]
    public long OutInvoice { get; set; }
    // سریال فاکتور
    [DataMember]
    public string OutNumber { get; set; }
}