using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.State
{
    public class SteteDto
    {
        public int Id { get; set; }

        public string StateTitle { get; set; } = null!;

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }
    }
}
