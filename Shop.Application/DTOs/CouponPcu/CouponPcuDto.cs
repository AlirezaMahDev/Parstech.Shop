using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.CouponPcu
{
	public class CouponPcuDto
	{
		public int Id { get; set; }

		public string Type { get; set; } = null!;

		public bool YesOrNo { get; set; }

		public int FkId { get; set; }

		public int CouponId { get; set; }
	}

	public class CouponListPcuDto
	{
		public string Type { get; set; }
		public List<CouponCheckPcuDto> list { get; set; }
	}
	public class CouponCheckPcuDto
	{
		public int FkId { get; set; }

		public int CouponId { get; set; }
	}



}
