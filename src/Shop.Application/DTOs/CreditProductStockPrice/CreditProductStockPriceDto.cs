using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.CreditProductStockPrice
{
    public class CreditProductStockPriceDto
    {
        public int Id { get; set; }

        public int ProductStockPriceId { get; set; }

        public int Persent { get; set; }

        public int Month { get; set; }

        public long PrePay { get; set; }
        public string TextPrePay { get; set; }

        public long PayMonth { get; set; }
        public string TextPayMonth { get; set; }

        public long Total { get; set; }
        public string TextTotal { get; set; }

        public string? Description { get; set; }

        public bool Active { get; set; }
        public string Name { get; set; } = null!;
        public string VariationName { get; set; } = null!;
        public string? Code { get; set; }
        public long SalePrice { get; set; }
        public long DiscountPrice { get; set; }
    }
}
