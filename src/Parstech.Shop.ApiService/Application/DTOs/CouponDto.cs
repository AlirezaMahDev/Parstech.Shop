using Shop.Application.DTOs.CouponType;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.Coupon
{
	public class CouponDto
	{
		public int Id { get; set; }

		public int CouponTypeId { get; set; }
		public string CouponTypeName { get; set; }

		public string Code { get; set; } = null!;

		public string ExpireDate { get; set; }
		public string ExpireDateShamsi { get; set; }

		public bool JustNewUser { get; set; }

		public long? MinPrice { get; set; }

		public long? MaxPrice { get; set; }

		public bool TwoUseSameTime { get; set; }

		public int? LimitUse { get; set; }

		public int? LimitEachUser { get; set; }

		public string Categury { get; set; } = null!;

		public string Products { get; set; } = null!;

		public string Users { get; set; } = null!;
		
        public int Persent { get; set; }

        public long Amount { get; set; }
    }

	

	public class ObjectCoupon
	{
		public string Status { get; set; }
		public List<int> idList { get; set; }
	}
}
