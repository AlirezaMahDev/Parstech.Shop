using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.UserProduct
{
    public class UserProductDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int UserId { get; set; }

        public string Type { get; set; } = null!;
    }
}
