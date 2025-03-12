using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.RepresentationType
{
    public class RepresentationTypeDto
    {
        public int Id { get; set; }

        public string Type { get; set; } = null!;

        public string Color { get; set; } = null!;
    }
}
