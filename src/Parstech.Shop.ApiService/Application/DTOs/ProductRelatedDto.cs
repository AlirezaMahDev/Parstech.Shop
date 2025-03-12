using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.ProductRelated
{
    public class ProductRelatedDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int FkProductId { get; set; }
    }
}
