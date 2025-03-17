using System.Runtime.Serialization;

namespace Parstech.Shop.Shared.DTOs;

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