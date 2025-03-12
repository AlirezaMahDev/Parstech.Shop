using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.IRole
{
    public class IRoleDto
    {
        public string Id { get; set; } = null!;

        public string? Name { get; set; }

        public string? PersianName { get; set; }
    }
}
