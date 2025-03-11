using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.Status
{
	public class StatusDto
	{
		public int Id { get; set; }

		public string StatusName { get; set; } = null!;

		public string StatusLatinName { get; set; } = null!;

		public string? Icon { get; set; }

		public int? Olaviyat { get; set; }
	}
}
