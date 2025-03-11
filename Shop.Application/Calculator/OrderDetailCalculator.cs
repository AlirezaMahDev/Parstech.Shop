using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Calculator
{
	public static class OrderDetailCalculator
	{
		public static long GetDetailSum(long Price,int Count)
		{
			return Price * Count;
		} 
		public static long GetTotal(long sum,long Tax,long Discount)
		{
			return (sum+Tax)-Discount;
		} 
	}
}
