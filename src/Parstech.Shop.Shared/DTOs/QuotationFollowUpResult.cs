using System.Runtime.Serialization;

namespace Parstech.Shop.Shared.DTOs;

public class QuotationFollowUpResult
{
    // شناسه فاکتور
    [DataMember]
    public long OutInvoice { get; set; }

    // سریال فاکتور
    [DataMember]
    public string OutNumber { get; set; }
}