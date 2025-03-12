using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.Representation
{
    public class RepresentationDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int? StateId { get; set; }
    }

    public class QuickEditDto
    {
        public int id { get; set; }

        public long price { get; set; } 

        public int quantity { get; set; }
    }
}
