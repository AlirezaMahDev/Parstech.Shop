using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Contracts.Persistance;
using Shop.Domain.Models;
using Shop.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Persistence.Repositories
{
	public class OrderStatusRepository : GenericRepository<OrderStatus>, IOrderStatusRepository
	{
		private readonly DatabaseContext _context;
		public OrderStatusRepository(DatabaseContext context) : base(context)
		{
			_context = context;
		}

		public async Task CancelActiveAllOrderStatusesByOrderId(int orderId)
		{
			var allOrderStatuses = await _context.OrderStatuses.Where(a => a.OrderId == orderId).ToListAsync();
			foreach (var orderStatus in allOrderStatuses)
			{
				orderStatus.IsActive = false;
				await UpdateAsync(orderStatus);
			}
		}

		public async Task<OrderStatus?> GetActiveOrderStatuseByOrderId(int orderId)
		{
			return await _context.OrderStatuses.Include(a => a.Status).FirstOrDefaultAsync(a => a.OrderId == orderId && a.IsActive);
		}

        public async Task<List<OrderStatus>> GetByOrderId(int OrderId)
        {
            if (_context.OrderStatuses.Any(u => u.OrderId == OrderId))
            {
                return await _context.OrderStatuses.Where(u => u.OrderId == OrderId).ToListAsync();
            }
            else
            {
                var orderStatuses = new List<OrderStatus>();
                return orderStatuses;
            }
        }

        public async Task<bool> CheckCancelationStatusForOrder(int orderId)
		{
			if (await _context.OrderStatuses.AnyAsync(u => u.OrderId == orderId && (u.StatusId == 6 || u.StatusId == 9)))
			{
				return true;
			}
			return false;
		}
	}
}
	