﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.UserBilling
{
    public class UserBillingDto
    {
        public int? Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Company { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string Mobile { get; set; } = null!;

        public string? Country { get; set; }

        public string? State { get; set; }

        public string? City { get; set; }

        public string? Address { get; set; }

        public string? PostCode { get; set; }

        public string? EconomicCode { get; set; }

        public string? NationalCode { get; set; }
        public int UserId { get; set; }
    }
}
