using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.WalletTypes
{
    public class WalletTypesDto
    {
        public int TypeId { get; set; }

        public string TypeTitle { get; set; } = null!;

        public string? Color { get; set; }
    }
}
