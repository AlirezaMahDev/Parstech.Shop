using System.Runtime.Serialization;

namespace Parstech.Shop.Context.Application.DTOs.Rahkaran;

public class RahkaranFollowUpRequest
{
    // شناسه پیش فاکتور
    [DataMember]
    public long IDQ { get; set; }
}

[DataContract]
[Serializable]
public class RahkaranFollowResult
{
    // شناسه فاکتور
    [DataMember]
    public long OutInvoice { get; set; }
    // سریال فاکتور
    [DataMember]
    public string OutNumber { get; set; }
}