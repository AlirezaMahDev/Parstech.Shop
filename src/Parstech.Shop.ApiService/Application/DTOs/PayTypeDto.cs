using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.PayType
{
    public class PayTypeDto
    {
        public int Id { get; set; }

        public string TypeName { get; set; } = null!;

        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}
