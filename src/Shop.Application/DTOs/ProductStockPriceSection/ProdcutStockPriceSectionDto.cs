using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.ProductStockPriceSection
{
    public class ProdcutStockPriceSectionDto
    {
        public int ProdutSrockPriceId { get; set; }
        public bool? ShowInDiscountPanels { get; set; }
        public List<SectionDto> sections { get; set; }
    }


    public class SectionDto
    {
        public int Id { get; set; }

        public int SectionId { get; set; } 
        public string SectionName { get; set; } 
    }
}
