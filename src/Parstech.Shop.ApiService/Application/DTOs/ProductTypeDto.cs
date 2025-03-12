using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.ProductType
{
    public class ProductTypeDto
    {
        public int Id { get; set; }

        public string TypeName { get; set; } = null!;
    }
}
