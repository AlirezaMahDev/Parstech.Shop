using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.Tax
{
    public class TaxDto
    {
        public int Id { get; set; }

        public string TaxName { get; set; } = null!;
    }
}
