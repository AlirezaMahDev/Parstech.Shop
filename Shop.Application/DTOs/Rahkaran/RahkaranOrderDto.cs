using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.Rahkaran
{
    public class RahkaranOrderDto
    {
        public int OrderId { get; set; }
        public string OrderCode { get; set; }
       
        public string? RahkaranPishNumber { get; set; }

        public string? RahakaranFactorNumber { get; set; }

        public string? RahakaranFactorSerial { get; set; }
    }
}
