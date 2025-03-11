using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.OrderStatus
{
	public class OrderStatusDto
	{
		public int Osid { get; set; }

		public int StatusId { get; set; }

		public DateTime CreateDate { get; set; }

		public string? FileName { get; set; }
		public IFormFile File { get; set; }

		public string? Comment { get; set; }

		public string CreateBy { get; set; } = null!;

		public bool IsActive { get; set; }

		public int OrderId { get; set; }
	}
	public class StatusOfOrderDto
	{
        public int OrderId { get; set; }
        public int StatusId { get; set; }
        public DateTime CreateDate { get; set; }
		public string? FileName { get; set; }
		public string CreateDateShamsi { get; set; }
        public string StatusName { get; set; }
        public string CreateBy { get; set; } = null!;
    }
}
