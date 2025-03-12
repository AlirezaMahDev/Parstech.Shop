using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.Property
{
    public class PropertyDto
    {
        public int Id { get; set; }

        public string Caption { get; set; } = null!;

        public int PropertyCateguryId { get; set; }
        public string PropertyCateguryTitle { get; set; }

        public int CateguryId { get; set; }
        public string CateguryTitle { get; set; }
    }

    public class PropertyParameterDto
    {
        public int CurrentPage { get; set; }
        public int TakePage { get; set; }
        public int PageCount { get; set; }
        public string Filter { get; set; }
        public int categuryId { get; set; }
        public int propertyCateguryId { get; set; }
    }

    
}
