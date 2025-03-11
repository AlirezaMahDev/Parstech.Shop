using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.Rahkaran
{
    public class RahkaranUserDto
    {
        public int Id { get; set; }

        public string UserName { get; set; } 
        public string? FirstName { get; set; }

        public string? LastName { get; set; }


        public string? EconomicCode { get; set; }

        public string? NationalCode { get; set; }

        public int? UserId { get; set; }

        public string? RahkaranUserId { get; set; }
    }
}
