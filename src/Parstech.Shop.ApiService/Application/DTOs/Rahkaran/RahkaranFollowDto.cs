using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.Rahkaran
{
    

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
}
