using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.Api
{
    public class SsoDto
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string userCode { get; set; }
        public string nationalNumber { get; set; }
        public string phone { get; set; }
        public string orgLevel { get; set; }
    }
}
