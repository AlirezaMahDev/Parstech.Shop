using System.Runtime.Serialization;

namespace Parstech.Shop.Shared.DTOs;

public class RahkaranFollowUpRequest
{
    // شناسه پیش فاکتور
    [DataMember]
    public long IDQ { get; set; }
}