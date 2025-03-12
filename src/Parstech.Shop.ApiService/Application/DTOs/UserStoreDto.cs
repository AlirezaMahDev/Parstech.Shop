using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.UserStore
{
    public class UserStoreDto
    {
        public int Id { get; set; }

        public string UserName { get; set; } = null!;

        public string StoreName { get; set; } = null!;
        public string LatinStoreName { get; set; } = null!;

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Phone { get; set; }

        public string? Mobile { get; set; }

        public string? Country { get; set; }

        public string? State { get; set; }

        public string? City { get; set; }

        public string? Address { get; set; }

        public string? PostCode { get; set; }

        public int UserId { get; set; }
        public int RepId { get; set; }
        public int? PersentOfSale { get; set; }
    }
}
