using System.Runtime.Serialization;

namespace Parstech.Shop.Shared.DTOs;

public class QuotationFollowUpRequest
{
    // شناسه پیش فاکتور
    [DataMember]
    public long IDQ { get; set; }
}