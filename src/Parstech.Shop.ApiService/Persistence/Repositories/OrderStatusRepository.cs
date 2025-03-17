using Microsoft.EntityFrameworkCore;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Domain.Models;
using Parstech.Shop.ApiService.Persistence.Context;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

public class OrderStatusRepository : GenericRepository<OrderStatus>, IOrderStatusRepository
{
    private readonly DatabaseContext _context;

    public OrderStatusRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }

    public async Task CancelActiveAllOrderStatusesByOrderId(int orderId)
    {
        List<OrderStatus>? allOrderStatuses =
            await _context.OrderStatuses.Where(a => a.OrderId == orderId).ToListAsync();
        foreach (OrderStatus? orderStatus in allOrderStatuses)
        {
            orderStatus.IsActive = false;
            await UpdateAsync(orderStatus);
        }
    }

    public async Task<OrderStatus?> GetActiveOrderStatuseByOrderId(int orderId)
    {
        return await _context.OrderStatuses.Include(a => a.Status)
            .FirstOrDefaultAsync(a => a.OrderId == orderId && a.IsActive);
    }

    public async Task<List<OrderStatus>> GetByOrderId(int OrderId)
    {
        if (_context.OrderStatuses.Any(u => u.OrderId == OrderId))
        {
            return await _context.OrderStatuses.Where(u => u.OrderId == OrderId).ToListAsync();
        }
        else
        {
            List<OrderStatus>? orderStatuses = new();
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