using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Persistence.Context;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

public class OrderShippingRepository : GenericRepository<OrderShipping>, IOrderShippingRepository
{
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;

    public OrderShippingRepository(DatabaseContext context, IMapper mapper) : base(context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OrderShipping> GetOrderShippingByOrderId(int orderId)
    {
        OrderShipping result = new();
        if (await _context.OrderShippings.AnyAsync(a => a.OrderId == orderId))
        {
            result = await _context.OrderShippings.FirstOrDefaultAsync(a => a.OrderId == orderId);
        }

        return result;
    }
}