using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Persistence.Context;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

public class OrderPayRepository : GenericRepository<OrderPay>, IOrderPayRepository
{
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;
    private readonly IOrderStatusRepository _orderStatusRepo;

    public OrderPayRepository(DatabaseContext context, IMapper mapper, IOrderStatusRepository orderStatusRepo) :
        base(context)
    {
        _context = context;
        _mapper = mapper;
        _orderStatusRepo = orderStatusRepo;
    }

    public async Task<OrderPay> GetByOrderId(int OrderId)
    {
        if (_context.OrderPays.Any(u => u.OrderId == OrderId))
        {
            return await _context.OrderPays.FirstOrDefaultAsync(u => u.OrderId == OrderId);
        }
        else
        {
            OrderPay orderPay = new();
            return orderPay;
        }
    }

    public async Task<List<OrderPay>> GetListByOrderId(int OrderId)
    {
        if (_context.OrderPays.Any(u => u.OrderId == OrderId))
        {
            return await _context.OrderPays.Where(u => u.OrderId == OrderId).ToListAsync();
        }
        else
        {
            List<OrderPay> orderPay = new();
            return orderPay;
        }
    }

    public async Task<bool> HasOrderPay(int OrderId)
    {
        if (await _context.OrderPays.AnyAsync(u => u.OrderId == OrderId))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}